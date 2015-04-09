using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryFog.NameParser
{
    /// <summary>
    /// Parse a person full name 
    /// </summary>
    /// <example>
    /// 1. Mr Jack Johnson  => Title = "Mr", First Name = "Jack" Last Name = "Johnson"
    /// 2. Jack Johnson  => First Name = "Jack" Last Name = "Johnson"
    /// 3. Jack => First Name = "Jack"
    /// 4. Jack Johnson Enterprises => ignored
    /// 5. Pasquale (Pat) Vacoturo  =>  First Name = "Pasquale" Last Name = "Vacoturo" Nickname = Pat 
    /// </example>
    /// <remarks>
    /// 1. The prefix "ATTN:" is removed if exists and the parsing proceeds on the new string
    /// </remarks>
    public class FullNameParser
    {
        ///TODO: Improve searching for multiple words last names ( example Van Der Graaf ).

        string fullName;

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
        public string NickName { get; private set; }
        public string DisplayName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FullNameParser"/> class.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        public FullNameParser(string fullName)
        {
            this.fullName = fullName;
        }

        public void Parse2()
        {
            var assembly = typeof(IPattern).;

            foreach (Type t in AppDomain.CurrentDomain.GetAssemblies().GetTypes())
            {
                if (t is IPattern) { }
            }
        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        public void Parse()
        {
            DisplayName = fullName;
            if (String.IsNullOrEmpty(fullName))
                return;

            RemoveATTNPrefixIfNeeded();

            if (CompanyNamePattern())
                return;

            if (TitleFirstLastPattern())
                return;

            if (TitleFirstLastSuffixPattern())
                return;

            if (TitleFirstInitialLastPattern())
                return;

            if (FirstLastPattern())
                return;

            if (FirstNameOnlyPattern())
                return;

            if (FirstNickLastPattern())
                return;


        }

        /// <summary>
        /// Removes the attn prefix if needed.
        /// </summary>
        private void RemoveATTNPrefixIfNeeded()
        {
            if (fullName.StartsWith("ATTN:"))
            {
                fullName = fullName.Substring(5).Trim();
            }

        }


        /// <summary>
        /// Search for names that resembles Companies names.
        /// </summary>
        /// <returns></returns>
        private bool CompanyNamePattern()
        {
            if (fullName.ToLower().Contains("ltd") ||
                fullName.ToLower().Contains("ltd.") ||
                fullName.ToLower().Contains("inc") ||
                fullName.ToLower().Contains("inc.") ||
                fullName.ToLower().Contains("limited") ||
                fullName.ToLower().Contains("limited."))
                return true;

            return false;
        }


        /// <summary>
        /// Seek for this pattern : Title firstname lastname .
        /// </summary>
        /// <returns></returns>
        private bool TitleFirstLastPattern()
        {
            //Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
            Match match = Regex.Match(fullName, @"^(?<title>(mr|mr\W?|ms|ms\W?|mrs|mrs\W?)) (?<first>\w+) (?<last>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                Title = match.Groups["title"].Value;
                FirstName = match.Groups["first"].Value;
                LastName = match.Groups["last"].Value;
                DisplayName = String.Format("{0} {1}", FirstName, LastName);
                
            }

            return match.Success;
        }

        /// <summary>
        /// Seek for this pattern : Title firstname initial lastname .
        /// </summary>
        /// <returns></returns>
        private bool TitleFirstInitialLastPattern()
        {
            //Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
            Match match = Regex.Match(fullName, @"^(?<title>(mr|mr\W?|ms|ms\W?|mrs|mrs\W?)) (?<first>\w+) (?<initial>[a-z]+|[a-z]\W?) (?<last>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                Title = match.Groups["title"].Value;
                FirstName = match.Groups["first"].Value;
                LastName = match.Groups["last"].Value;
                DisplayName = String.Format("{0} {1}", FirstName, LastName);

            }

            return match.Success;
        }


        /// <summary>
        /// Seek for this pattern : Title firstname lastname suffix.
        /// </summary>
        /// <returns></returns>
        private bool TitleFirstLastSuffixPattern()
        {
            //Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
            //Suffix should be I or II or III or Jr. or Jr or Sr. or Sr
            Match match = Regex.Match(fullName, @"^(?<title>(mr|mr\W?|ms|ms\W?|mrs|mrs\W?)) (?<first>\w+) (?<last>\w+) (?<suffix>(i|ii|iii|jr|jr\W?|sr|sr\W?\W?))$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                Title = match.Groups["title"].Value;
                FirstName = match.Groups["first"].Value;
                LastName = match.Groups["last"].Value;
                DisplayName = String.Format("{0} {1}", FirstName, LastName);

            }

            return match.Success;
        }

        private bool FirstLastPattern()
        {
            Match match = Regex.Match(fullName, @"^(?<first>\w+) (?<last>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                FirstName = match.Groups["first"].Value;
                LastName = match.Groups["last"].Value;
                DisplayName = String.Format("{0} {1}", FirstName, LastName);
            }
            return match.Success;
        }

        private bool FirstNameOnlyPattern()
        {
            Match match = Regex.Match(fullName, @"^(?<first>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                FirstName = match.Groups["first"].Value;
                DisplayName = fullName;
            }
            return match.Success;
        }

        private bool FirstNickLastPattern()
        {
            Match match = Regex.Match(fullName, @"^(?<first>\w+) \((?<nick>\w+)\) (?<last>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                FirstName = match.Groups["first"].Value;
                LastName = match.Groups["last"].Value;
                NickName = match.Groups["nick"].Value;
                DisplayName = String.Format("{0} {1}", FirstName, LastName);
            }

            return match.Success;
        }
        
    }
}
