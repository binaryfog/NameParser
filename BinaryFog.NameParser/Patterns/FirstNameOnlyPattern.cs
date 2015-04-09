using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstNameOnlyPattern : IPattern
    {
        public ParsedName Parse(string rawName)
        {
            Match match = Regex.Match(rawName, @"^(?<first>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName()
                {
                    FirstName = match.Groups["first"].Value,
                    DisplayName = rawName,
                    Score = 100
                };
                return pn;

            }
            return null;
        }
    }
}
