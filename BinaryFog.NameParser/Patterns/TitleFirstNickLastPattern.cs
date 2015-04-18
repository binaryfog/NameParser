using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class TitleFirstNickLastPattern : IPattern
    {

        public ParsedName Parse(string rawName)
        {
            Match match = Regex.Match(rawName, @"^(?<title>(mr|mr\W?|ms|ms\W?|mrs|mrs\W?)) (?<first>\w+) \((?<nick>\w+)\) (?<last>\w+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                ParsedName pn = new ParsedName()
                {
                    Title = match.Groups["title"].Value,
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
