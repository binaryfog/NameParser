using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstLastSuffixPattern : IPattern
    {
        public ParsedName Parse(string rawName)
        {
            //Suffix should be I or II or III or Jr. or Jr or Sr. or Sr or ESQ or ESQ. or ESQ"
            Match match = Regex.Match(rawName, @"^(?<first>\w+) (?<last>\w+) (?<suffix>(i|ii|iii|jr|jr\W?|sr|sr\W?}|esq|esq\W?|esq""\W?))$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName()
                {
                    FirstName = match.Groups["first"].Value,
                    LastName = match.Groups["last"].Value,
                    DisplayName = String.Format("{0} {1}", match.Groups["first"].Value, match.Groups["last"].Value),
                    Suffix = match.Groups["suffix"].Value,
                    Score = 100
                };
                return pn;
            }

            return null;
        }
    }
}
