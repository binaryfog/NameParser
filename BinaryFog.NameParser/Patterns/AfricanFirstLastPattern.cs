using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class AfricanFirstLastPattern : IFullNamePattern
    {
        private static readonly string Pattern = $@"^{AfricanFirst}{Space}{Last}$";
        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
            if (!match.Success) return null;


            var firstName = match.Groups["first"]?.Value;
            var lastPart = match.Groups["last"]?.Value;
            var lastName = $"{lastPart}";

            var pn = new ParsedFullName
            {
                FirstName = firstName,
                LastName = lastName,
                DisplayName = $"{firstName} {lastName}",
                Score = 1200
            };
            return pn;
        }
    }
}