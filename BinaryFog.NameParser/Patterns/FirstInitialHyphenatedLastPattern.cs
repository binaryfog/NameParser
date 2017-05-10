using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstInitialHyphenatedLastPattern : IPattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + Initial + Space + LastHyphenated + @"$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ParsedName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var pn = new ParsedName(this.GetType().Name)
            {
                FirstName = match.Groups["first"].Value,
                MiddleName = match.Groups["initial"].Value.Replace(".", string.Empty),
                LastName = $"{match.Groups["lastPart1"].Value}-{match.Groups["lastPart2"].Value}",
                DisplayName = $"{match.Groups["first"].Value} {match.Groups["lastPart1"].Value}-{match.Groups["lastPart2"].Value}",
                Score = 100
            };
            return pn;
        }
    }
}
