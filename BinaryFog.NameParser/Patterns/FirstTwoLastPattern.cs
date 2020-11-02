using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class FirstTwoLastPattern : IFullNamePattern
    {
        private static readonly string Pattern = $@"^{First}{Space}(?<last1>{Name}){Space}(?<last2>{Name})$";
        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;

            var lastPart1 = match.Groups["last1"].Value;
            var lastPart2 = match.Groups["last2"].Value;

            var lastName = $"{lastPart1} {lastPart2}";

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = $"{firstName} {lastName}",
                Score = 25 + scoreMod
            };
            return pn;
        }
    }
}