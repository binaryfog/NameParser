using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstMiddlePrefixedLastPattern : IPattern
    {
        public ParsedName Parse(string rawName)
        {

            string[] parts = Utils.GetAllPrefixes();            

            foreach (string part in parts)
            {
                StringBuilder patternBuilder = new StringBuilder(@"^(?<first>\w+) (?<middle>\w+) (?<prefix>");
                patternBuilder.Append(part);
                patternBuilder.Append(@"+) (?<last>\w+)$");

                string regexPattern = patternBuilder.ToString();
                
                Match match = Regex.Match(rawName, regexPattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string prefix = match.Groups["prefix"].Value;
                    ParsedName pn = new ParsedName()
                    {
                        
                        FirstName = match.Groups["first"].Value,
                        MiddleName = match.Groups["middle"].Value,

                        LastName = prefix + " " + match.Groups["last"].Value,
                        DisplayName = String.Format("{0} {1} {2}", match.Groups["first"].Value, prefix, match.Groups["last"].Value),
                        Score = 200
                    };
                    return pn;
                }
            }
            return null;
        }
    }
}
