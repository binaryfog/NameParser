using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstNickMiddleHyphenatedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + OptionalSpace + Nick + OptionalSpace + Middle + Space + LastHyphenated + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
            if (rawName == null) return null;
            var match = Rx!.Match(rawName);
            if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			var nickName = match.Groups["nick"].Value;
			var middleName = match.Groups["middle"].Value;
			var lastPart1 = match.Groups["last1"].Value;
			var lastPart2 = match.Groups["last2"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstNames(ref scoreMod, firstName, middleName);
			ModifyScoreExpectedName(ref scoreMod, nickName);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

			var pn = new ParsedFullName {
				FirstName = firstName,
				NickName = nickName,
				MiddleName = middleName,
				LastName = $"{lastPart1}-{lastPart2}",
				DisplayName = $"{firstName} {middleName} {lastPart1}-{lastPart2}",
				Score = 175 + scoreMod
			};
			return pn;
		}
	}
}