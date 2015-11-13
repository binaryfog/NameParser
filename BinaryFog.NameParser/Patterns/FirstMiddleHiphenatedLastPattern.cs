using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstMiddleHiphenatedLastPattern : IPattern
    {

        public ParsedName Parse(string rawName)
        {
            Match match = Regex.Match(rawName, @"^(?<first>\w+) (?<middle>\w+) (?<lastPart1>\w+)-(?<lastPart2>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName()
                {
                    FirstName = match.Groups["first"].Value,
                    MiddleName = match.Groups["middle"].Value,
                    LastName = String.Format( "{0}-{1}", match.Groups["lastPart1"].Value, match.Groups["lastPart2"].Value ),
                    DisplayName = String.Format("{0} {1}-{2}", match.Groups["first"].Value, match.Groups["lastPart1"].Value, match.Groups["lastPart2"].Value),
                    Score = 100
                };
                return pn;
                
            }
            return null;
        }
    }
}
