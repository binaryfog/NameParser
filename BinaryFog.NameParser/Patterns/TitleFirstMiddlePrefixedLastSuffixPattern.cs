using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;

namespace BinaryFog.NameParser.Patterns
{
    internal class TitleFirstMiddlePrefixedLastSuffixPattern : IPattern
    {
        public ParsedName Parse(string rawName)
        {

            string[] parts = Utils.GetAllPrefixes();            

            foreach (string part in parts)
            {
                //Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
                StringBuilder patternBuilder = new StringBuilder(@"^(?<title>(mr|mr\W?|ms|ms\W?|mrs|mrs\W?|sr|sr\W?)) (?<first>\w+) (?<middle>\w+) (?<prefix>");
                patternBuilder.Append(part);
                patternBuilder.Append(@"+) (?<last>\w+),?\s*(?<suffix>(i|ii|iii|jr|jr\W?|sr|sr\W?|esq|esq\W?|esq""|jr\sesq\W?|jr\sesq|sr\sesq|sr\sesq\W?))$");

                string regexPattern = patternBuilder.ToString();
                
                Match match = Regex.Match(rawName, regexPattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string prefix = match.Groups["prefix"].Value;
                    ParsedName pn = new ParsedName()
                    {
                        Title = match.Groups["title"].Value,
                        FirstName = match.Groups["first"].Value,
                        MiddleName = match.Groups["middle"].Value,
                        LastName = prefix + " " + match.Groups["last"].Value,
                        Suffix = match.Groups["suffix"].Value,
                        DisplayName = String.Format("{0} {1} {2}", match.Groups["first"].Value, prefix, match.Groups["last"].Value),
                        Score = 300
                    };
                    return pn;
                }
            }
            return null;
        }
    }
}
