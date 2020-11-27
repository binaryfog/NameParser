﻿using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns
{
    [UsedImplicitly]
    public class TitleFirstLastSuffixPattern : IFullNamePattern
    {
        private static readonly string Pattern =
            $@"^{Title}{Space}{First}{Space}{Last}{OptionalCommaSpace}{Suffix}$";

        private static readonly Regex Rx = new Regex(Pattern, CommonPatternRegexOptions);

        public ParsedFullName Parse(string rawName)
        {
            //Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
            //Suffix should be I or II or III or Jr. or Jr or Sr. or Sr or ESQ or ESQ. or ESQ"
            var match = Rx.Match(rawName);
            if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            var lastName = match.Groups["last"].Value;
            var scoreMod = 0;
            ModifyScoreExpectedFirstName(ref scoreMod, firstName);
            ModifyScoreExpectedLastName(ref scoreMod, lastName);

            var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
                FirstName = firstName,
                LastName = lastName,
                DisplayName = $"{firstName} {lastName}",
                Suffix = match.Groups["suffix"].Value,
                Score = 100 + scoreMod
            };
            return pn;
        }
    }
}