using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class TitleFirstTwoMiddlePrefixedLastSuffixPattern : IFullNamePattern
    {
        private static readonly string Pattern =
            $@"^{Title}{Space}{First}{Space}(?<middle1>{Name}){Space}(?<middle2>{Name}){Space}{Prefix}{Space}{Last}{OptionalCommaSpace}{Suffix}$";

        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var prefix = match.Groups["prefix"].Value;
            var firstName = match.Groups["first"].Value;
            var middleName1 = match.Groups["middle1"].Value;
            var middleName2 = match.Groups["middle2"].Value;
            var lastPart = match.Groups["last"].Value;

            var middleName = $"{middleName1} {middleName2}";
            var lastName = $"{prefix} {lastPart}";

            var scoreMod = 0;
            ModifyScoreExpectedFirstNames(ref scoreMod, firstName, middleName1, middleName2);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart);

            var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = match.Groups["suffix"].Value,
                DisplayName = $"{firstName} {middleName} {lastName}",
                Score = 275 + scoreMod
            };
            return pn;
        }
    }
}