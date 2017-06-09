using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstNickMiddleHyphenatedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Nick + Space + Middle + Space + LastHyphenated + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			var nickName = match.Groups["nick"].Value;
			var middleName = match.Groups["middle"].Value;
			var lastPart1 = match.Groups["lastPart1"].Value;
			var lastPart2 = match.Groups["lastPart2"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName, 10);
			ModifyScoreExpectedName(ref scoreMod, nickName);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart1);
			ModifyScoreExpectedLastName(ref scoreMod, lastPart2);

			var pn = new ParsedFullName {
				FirstName = firstName,
				NickName = nickName,
				MiddleName = middleName,
				LastName = $"{lastPart1}-{lastPart2}",
				DisplayName = $"{firstName} {lastPart1}-{lastPart2}",
				Score = 175 + scoreMod
			};
			return pn;
		}
	}
}