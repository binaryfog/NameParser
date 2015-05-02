using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class CompanyPattern : IPattern
    {

        public ParsedName Parse(string rawName)
        {
            Match match = Regex.Match(rawName, @" (?<lastWord>(ltd|ltd\W?|inc|inc\W?|limited|limited\W?))$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName() 
                {
                    DisplayName = rawName,
                    Score = ParsedName.MaxScore
                };

                return pn;
            }

            return null;
        }
    }
}
