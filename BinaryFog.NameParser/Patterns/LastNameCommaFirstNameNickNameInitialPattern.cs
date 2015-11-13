using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BinaryFog.NameParser.Patterns
{
    internal class LastNameCommaFirstNameNickNameInitialPattern : IPattern
    {
        public ParsedName Parse(string rawName)
        {
            Match match = Regex.Match(rawName, @"^(?<last>\w+),\s*(?<first>\w+)\s*[\(']{1}(?<nick>\w+)[\)']{1} (?<initial>[a-z]{1}.?)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName()
                {
                    FirstName = match.Groups["first"].Value,
                    LastName = match.Groups["last"].Value,
                    NickName = match.Groups["nick"].Value,
                    
                    DisplayName = String.Format("{0} {1}", match.Groups["first"].Value, match.Groups["last"].Value),
                    Score = 100
                };
                return pn;

            }
            return null;
        }
    }
}
