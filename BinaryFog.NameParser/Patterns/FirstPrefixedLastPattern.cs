using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstPrefixedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Prefix + Space + Last + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
			var firstName = match.Groups["first"].Value;
			var lastName = match.Groups["last"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,

				LastName = $"{prefix} {lastName}",
				DisplayName = $"{firstName} {prefix} {lastName}",
				Score = 275 + scoreMod
			};
			return pn;
		}
	}
}
