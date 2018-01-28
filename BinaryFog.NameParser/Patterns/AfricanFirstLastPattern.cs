using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class AfricanFirstLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + AfricanFirst + Space + Last + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;


			var firstName = match.Groups["first"].Value;
			var lastPart = match.Groups["last"].Value;
			var lastName = $"{lastPart}";

			var pn = new ParsedFullName {
				FirstName = firstName,
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Score = 1200
			};
			return pn;
		}
	}
}