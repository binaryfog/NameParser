using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstTwoMiddleLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + TwoMiddle + Space + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var middleName1 = match.Groups["middle1"].Value;
			var middleName2 = match.Groups["middle2"].Value;
			var lastName = match.Groups["last"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName1, 10);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName2, 10);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = $"{middleName1} {middleName2}",
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Score = 50 + scoreMod
			};
			return pn;
		}
	}
}