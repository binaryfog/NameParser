using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
    [UsedImplicitly]
    public class CompanyPattern : IFullNamePattern
    {
        private static readonly string Pattern = $@"{Space}(?<lastWord>({CompanySuffixes})\W?)$";
        private static readonly Regex Rx = new Regex(Pattern!, CommonPatternRegexOptions);


        public ParsedFullName Parse(string rawName)
        {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
            if (!match.Success) return null;
            

            var pn = new ParsedFullName {
                DisplayName = rawName,
                Score = ParsedFullName.MaxScore
            };

            return pn;
        }
    }
}