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
                    DisplayName = rawName,
                    Score = 100
                };

                string[] lastNamesInUppercase = USCensusLastNamesResource.USCensusLastNames.Split('\r', '\n');

                string matchedName = match.Groups["first"].Value;

                int matchOccurences  = ( from c in lastNamesInUppercase
                               where c == matchedName.ToUpper()
                               select c).Count();

                if ( matchOccurences > 0)
                    pn.LastName = matchedName;
                else
                    pn.FirstName = matchedName;
                
                return pn;

            }
            return null;
        }
    }
}
