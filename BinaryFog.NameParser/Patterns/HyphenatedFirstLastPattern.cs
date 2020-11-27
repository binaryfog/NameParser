﻿using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class HyphenatedFirstLastPattern : IFullNamePattern
    {
        private static readonly string Pattern =
            $@"^(?<first1>\w+){HyphenOptionallySpaced}(?<first2>{Name}){Space}(?<last>{Name})$";

        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            var match = Rx.Match(rawName);
            if (!match.Success) return null;

            var firstPart1 = match.Groups["first1"].Value;
            var firstPart2 = match.Groups["first2"].Value;
            var firstName = $"{match.Groups["first1"].Value}-{match.Groups["first2"].Value}";
            var lastName = match.Groups["last"].Value;

            var scoreMod = 0;
            ModifyScoreExpectedFirstNames(ref scoreMod, firstPart1, firstPart2);
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