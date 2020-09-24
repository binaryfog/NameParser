using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstIrishLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + @"(?<irishPrefix>O'|Mc|Mac)" + Last + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
			if (!match.Success) return null;


			var firstName = match.Groups["first"].Value;
			var irishPrefix = match.Groups["irishPrefix"].Value;
			var lastPart = match.Groups["last"].Value;
			var lastName = $"{irishPrefix}{lastPart}";

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart);

			var pn = new ParsedFullName {
				FirstName = firstName,
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Score = 300 + scoreMod
			};
			return pn;
		}
	}
}