using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class LastNameCommaFirstNamePattern : IFullNamePattern
    {
        private static readonly string Pattern = $@"^{Last}{CommaSpace}{First}$";
        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            var lastName = match.Groups["last"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = $"{firstName} {lastName}",
                Score = 100 + scoreMod
            };
            return pn;
        }
    }
}