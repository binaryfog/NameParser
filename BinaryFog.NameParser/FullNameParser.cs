using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace BinaryFog.NameParser
{
    using static Helpers;

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
    public class FullNameParser {
		public IReadOnlyList<ParsedFullName> Results { get; set; } = new ParsedFullName[0];

		protected string FullName { get; private set; }

		protected static Type PatternType { get; } = typeof(IFullNamePattern);

		private static bool _enableThirdParty;

		[UsedImplicitly]
		public static bool EnableAutomaticThirdPartyIntegration {
			get => _enableThirdParty;
			set {
				_enableThirdParty = value;
				BuildPatternsMap();
			}
		}

		private static void BuildPatternsMap() {
			PatternsMap = (_enableThirdParty
					? KnownAssemblies
						.Where(a
							=> !a.FullName.StartsWith("System.")
								&& !a.FullName.StartsWith("Microsoft."))
						.SelectMany(s => TryOrDefault(s.GetExportedTypes, Type.EmptyTypes))
					: typeof(FullNameParser).GetTypeInfo().Assembly.GetExportedTypes()
				)
				.Where(p => PatternType.IsAssignableFrom(p))
				.Select(t => t.GetConstructor(Type.EmptyTypes)?.Invoke(null))
				.OfType<IFullNamePattern>();
		}

		static FullNameParser() {
			BuildPatternsMap();
		}

		protected static IEnumerable<IFullNamePattern> PatternsMap { get; private set; }


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
		public FullNameParser(string fullName) {
			FullName = fullName;
		}

		public static FullNameParser Parse(string fullName) {
			var name = new FullNameParser(fullName);
			name.Parse();
			return name;
		}

		/// <summary>
		/// Parses this instance.
		/// </summary>
		public void Parse() {
			DisplayName = FullName;
			if (string.IsNullOrWhiteSpace(FullName))
				return;

			Preparse();

			Results = PatternsMap
				.Select(pattern => pattern.Parse(FullName))
				.Where(NotNull)
				.OrderByDescending(result => result?.Score ?? 0)
				.ToImmutableArray();

			var selectedResult = Results.FirstOrDefault();

			FirstName = selectedResult?.FirstName;
			MiddleName = selectedResult?.MiddleName;
			LastName = selectedResult?.LastName;
			Title = selectedResult?.Title;
			NickName = selectedResult?.NickName;
			Suffix = selectedResult?.Suffix;
			DisplayName = selectedResult?.DisplayName ?? FullName;
		}

		/// <summary>
		/// Removes the attn prefix if needed.
		/// </summary>
		protected void Preparse() {
			if (FullName.StartsWith("ATTN:", StringComparison.OrdinalIgnoreCase))
				FullName = FullName.Substring(5).Trim();
		}
	}
}