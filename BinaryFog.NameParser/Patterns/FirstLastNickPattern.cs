using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.NameComponentSets;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class FirstLastNickPattern : IFullNamePattern
    {
        private static readonly string Pattern = $@"^{First}{Space}{Last}{OptionalSpace}{Nick}$";
        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
            if (!match.Success) return null;

            var firstName = match.Groups["first"].Value;
            var lastName = match.Groups["last"].Value;
            var nickName = match.Groups["nick"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);
            ModifyScoreExpectedName(ref scoreMod, nickName);

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                LastName = lastName,
                NickName = nickName,
                DisplayName = $"{firstName} {lastName}",
                Score = 0 + scoreMod
            };
            return pn;
        }
    }
}