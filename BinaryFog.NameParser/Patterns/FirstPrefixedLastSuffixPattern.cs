using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.NameComponentSets;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstPrefixedLastSuffixPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Prefix + Space + Last + Space + Suffix + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
			var firstName = match.Groups["first"].Value;
			var lastPart = match.Groups["last"].Value;

			var lastName = $"{prefix} {lastPart}";

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart);
			var pn = new ParsedFullName {
				FirstName = firstName,
				LastName = lastName,
				Suffix = match.Groups["suffix"].Value,
				DisplayName = $"{firstName} {lastName}",
				Score = 275 + scoreMod
			};
			return pn;
		}
	}
}