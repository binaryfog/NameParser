using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstMiddleLastSuffixPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Middle + Space + Last + Space + Suffix + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var middleName = match.Groups["middle"].Value;
			var lastName = match.Groups["last"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstNames(ref scoreMod, firstName, middleName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				Suffix = match.Groups["suffix"].Value,
				DisplayName = $"{firstName} {middleName} {lastName}",
				Score = 100 + scoreMod
			};
			return pn;
		}
	}
}