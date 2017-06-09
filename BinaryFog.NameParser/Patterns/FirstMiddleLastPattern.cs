using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstMiddleLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Middle + Space + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var middleName = match.Groups["middle"].Value;
			var lastName = match.Groups["last"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName, 10);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Score = 10 + scoreMod
			};
			return pn;
		}
	}
}
