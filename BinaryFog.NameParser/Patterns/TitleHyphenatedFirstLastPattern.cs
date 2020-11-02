using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class TitleHyphenatedFirstLastPattern : IFullNamePattern
    {
        private static readonly string Pattern =
            $@"^{Title}{Space}(?<first1>{Name})-(?<first2>{Name}){Space}(?<last>{Name})$";

        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;

            var firstName1 = match.Groups["first1"].Value;
            var firstName2 = match.Groups["first2"].Value;
            var lastName = match.Groups["last"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstNames(ref scoreMod, firstName1, firstName2);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
                FirstName = $"{firstName1}-{firstName2}",
                LastName = lastName,
                DisplayName = $"{firstName1}-{firstName2} {lastName}",
                Score = 200 + scoreMod
            };
            return pn;
        }
    }
}