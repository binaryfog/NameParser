using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstInitialPrefixedLastSuffixPattern : IPattern
    {
        private static readonly Regex Rx = new Regex(
            @"^" + First + Space + Initial + Space + Prefix + Space + Last + Space + Suffix + @"$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ParsedName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var prefix = match.Groups["prefix"].Value;
            var pn = new ParsedName(this.GetType().Name)
            {
                FirstName = match.Groups["first"].Value,
                MiddleName = match.Groups["initial"].Value.Replace(".", string.Empty),
                LastName = prefix + " " + match.Groups["last"].Value,
                Suffix = match.Groups["suffix"].Value,
                DisplayName = $"{match.Groups["first"].Value} {prefix} {match.Groups["last"].Value}",
                Score = 100
            };
            return pn;
        }
    }
}
