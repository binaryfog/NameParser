using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstNickHyphenatedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + OptionalSpace + Nick + OptionalSpace + LastHyphenated + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName = match.Groups["first"].Value;
			var nickName = match.Groups["nick"].Value;
			var lastPart1 = match.Groups["last1"].Value;
			var lastPart2 = match.Groups["last2"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedName(ref scoreMod, nickName);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart2);


			var pn = new ParsedFullName {
				FirstName = firstName,
				NickName = nickName,
				LastName = $"{lastPart1}-{lastPart2}",
				DisplayName = $"{firstName} {lastPart1}-{lastPart2}",
				Score = 75 + scoreMod
			};
			return pn;
		}
	}
}