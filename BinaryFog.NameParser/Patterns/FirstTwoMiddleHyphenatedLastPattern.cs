using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstTwoMiddleHyphenatedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + TwoMiddle + Space + LastHyphenated + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var middleName1 = match.Groups["middle1"].Value;
			var middleName2 = match.Groups["middle2"].Value;
			var lastPart1 = match.Groups["lastPart1"].Value;
			var lastPart2 = match.Groups["lastPart2"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName1, 10);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName2, 10);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

			var lastName = $"{lastPart1}-{lastPart2}";
			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = $"{middleName1} {middleName2}",
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Score = 75 + scoreMod
			};
			return pn;
		}
	}
}