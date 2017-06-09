using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstLastSuffixPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Last + OptionalCommaSpace + Suffix + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var lastName = match.Groups["last"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Suffix = match.Groups["suffix"].Value,
				Score = 200 + scoreMod
			};
			return pn;
		}
	}
}
