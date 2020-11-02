using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class TitleFirstInitialPrefixedLastSuffixPattern : IFullNamePattern
    {
        private static readonly string Pattern =
            $@"^{Title}{Space}{First}{Space}{Initial}{Space}{Prefix}{Space}{Last}{Space}{Suffix}$";

        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var title = match.Groups["title"].Value;
            var firstName = match.Groups["first"].Value;
            var middleName = $"{match.Groups["initial"]}.";
            var prefix = match.Groups["prefix"].Value;
            var lastPart = match.Groups["last"].Value;
            var lastName = $"{prefix} {lastPart}";

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastPart);

            var pn = new ParsedFullName
            {
                Title = title,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Suffix = match.Groups["suffix"].Value,
                DisplayName = $"{firstName} {middleName} {lastName}",
                Score = 100
            };
            return pn;
        }
    }
}