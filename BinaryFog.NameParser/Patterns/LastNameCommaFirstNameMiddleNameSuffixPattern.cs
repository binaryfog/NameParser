using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class LastNameCommaFirstNameMiddleNameSuffixPattern : IFullNamePattern
    {
        private static readonly string Pattern =
            $@"^{Last}{CommaSpace}{First}{Space}{Middle}{Space}{Suffix}$";

        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            var lastName = match.Groups["last"].Value;
            var middleName = match.Groups["middle"].Value;
            var suffix = match.Groups["suffix"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedName(ref scoreMod, middleName);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Suffix = suffix,

                DisplayName = $"{firstName} {lastName}",
                Score = 100 + scoreMod
            };
            return pn;
        }
    }
}