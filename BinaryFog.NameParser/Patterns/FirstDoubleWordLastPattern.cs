using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstDoubleWordLastPattern : IPattern
    {

        public ParsedName Parse(string rawName)
        {
            Match match = Regex.Match(rawName, @"^(?<first>\w+) (?<last1>\w+) (?<last2>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string firstName = match.Groups["first"].Value;
                if (!Utils.GetAllFirstNames().Contains(firstName))
                    return null;
                
                ParsedName pn = new ParsedName()
                {
                    FirstName = match.Groups["first"].Value,
                    LastName = String.Format("{0} {1}", match.Groups["last1"].Value, match.Groups["last2"].Value),
                    DisplayName = String.Format("{0} {1} {2}", match.Groups["first"].Value, match.Groups["last1"].Value, match.Groups["last2"].Value),
                    Score = 100
                };
                return pn;
                
            }
            return null;
        }
    }
}
