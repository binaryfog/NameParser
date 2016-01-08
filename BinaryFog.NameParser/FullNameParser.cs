using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryFog.NameParser {
	/// <summary>
	/// Parse a person full name 
	/// </summary>
	/// <example>
	/// 1. Mr Jack Johnson  => Title = "Mr", First Name = "Jack" Last Name = "Johnson"
	/// 2. Jack Johnson  => First Name = "Jack" Last Name = "Johnson"
	/// 3. Jack => First Name = "Jack"
	/// 4. Jack Johnson Enterprises => ignored
	/// 5. Pasquale (Pat) Vacoturo  =>  First Name = "Pasquale" Last Name = "Vacoturo" Nickname = Pat 
	/// 6. Mr Giovanni Van Der Hutte  => Title = "Mr", First Name = "Giovanni" Last Name = "Van Der Hutte"
	/// 7. Giovanni Van Der Hutte  => First Name = "Giovanni" Last Name = "Van Der Hutte"
	/// </example>
	/// <remarks>
	/// 1. The prefix "ATTN:" is removed if exists and the parsing proceeds on the new string
	/// </remarks>
	public class FullNameParser
    {
        Dictionary<Type, ParsedName> _results;

        string _fullName;
		private static readonly Type PatternType = typeof(IPattern);
		private static readonly IEnumerable<IPattern> PatternsMap =
			AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => p.IsClass && PatternType.IsAssignableFrom(p))
			.Select(t => t.GetConstructor(Type.EmptyTypes)?.Invoke(null)).OfType<IPattern>().Where(o=>o!=null);

		public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
        public string NickName { get; private set; }
        public string Suffix { get; private set; }
        public string DisplayName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FullNameParser"/> class.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        public FullNameParser(string fullName)
        {
            _fullName = fullName;
            _results = new Dictionary<Type, ParsedName>();

        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        public void Parse()
        {
            DisplayName = _fullName;
            if (string.IsNullOrEmpty(_fullName))
                return;

            RemoveAttnPrefixIfNeeded();

	        foreach (var pattern in PatternsMap)
            {
                var result = pattern.Parse(_fullName);
                _results.Add(pattern.GetType(), result);
            }

            var possibleAnswers = (
				from c in _results.Values.AsEnumerable()
				where c != null
				orderby c.Score descending
				select c).ToList();

            var v = possibleAnswers.FirstOrDefault();

            FirstName = v?.FirstName;
            MiddleName = v?.MiddleName;
            LastName = v?.LastName;
            Title = v?.Title;
            NickName = v?.NickName;
            Suffix = v?.Suffix;
            DisplayName = v?.DisplayName ?? _fullName;
               
            
        }

        /// <summary>
        /// Removes the attn prefix if needed.
        /// </summary>
        private void RemoveAttnPrefixIfNeeded()
        {
            if (_fullName.StartsWith("ATTN:",StringComparison.InvariantCultureIgnoreCase))
            {
                _fullName = _fullName.Substring(5).Trim();
            }

        }


        
    }
}
