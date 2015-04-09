using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class TitleFirstInitialLastPattern : IPattern
    {
        public ParsedName Parse(string rawName)
        {
            //Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
            Match match = Regex.Match(rawName, @"^(?<title>(mr|mr\W?|ms|ms\W?|mrs|mrs\W?)) (?<first>\w+) (?<initial>[a-z]{1}.?) (?<last>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName(){
                    Title = match.Groups["title"].Value,
                    FirstName = match.Groups["first"].Value,
                    LastName = match.Groups["last"].Value,
                    DisplayName = String.Format("{0} {1}", match.Groups["first"].Value, match.Groups["last"].Value),
                    Score = 100
                };
                return pn;
            }

            return null;
        }
    }
}
