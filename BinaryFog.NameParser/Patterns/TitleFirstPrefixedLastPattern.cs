using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class TitleFirstPrefixedLastPattern : IFullNamePattern
    {
        private static readonly string Pattern = $@"^{Title}{Space}{First}{Space}{Prefix}{Space}{Last}$";
        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var prefix = match.Groups["prefix"].Value;
            var firstName = match.Groups["first"].Value;
            var lastPart = match.Groups["last"].Value;
            var lastName = $"{prefix} {lastPart}";
            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart);
            var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
                FirstName = firstName,

                LastName = lastName,
                DisplayName = $"{firstName} {lastName}",
                Score = 275 + scoreMod
            };
            return pn;
        }
    }
}