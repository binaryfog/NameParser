using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class SingleHyphenatedNameOnlyPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + LastHyphenated + @"$",
			CommonPatternRegexOptions);


		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			
			var lastPart1 = match.Groups["last1"].Value;
			var lastPart2 = match.Groups["last2"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

			var lastName = $"{lastPart1}-{lastPart2}";

			var pn = new ParsedFullName {
				LastName = lastName,
				DisplayName = lastName,
				Score = 50 + scoreMod
			};

			return pn;
		}
	}
}