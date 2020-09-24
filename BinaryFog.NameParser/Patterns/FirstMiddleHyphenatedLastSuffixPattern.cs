using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstMiddleHyphenatedLastSuffixPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Middle + Space + LastHyphenated + Space + Suffix + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var middleName = match.Groups["middle"].Value;

			var lastPart1 = match.Groups["last1"].Value;
			var lastPart2 = match.Groups["last2"].Value;


			var scoreMod = 0;
			ModifyScoreExpectedFirstNames(ref scoreMod, firstName, middleName);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

			var lastName = $"{lastPart1}-{lastPart2}";
			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Suffix = match.Groups["suffix"].Value,
				DisplayName = $"{firstName} {middleName} {lastName}",
				Score = 10 + scoreMod
			};
			return pn;
		}
	}
}