using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstNickMiddleLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Nick + Space + Middle + Space + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			var nickName = match.Groups["nick"].Value;
			var middleName = match.Groups["middle"].Value;
			var lastName = match.Groups["last"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedName(ref scoreMod, nickName);
			ModifyScoreExpectedFirstName(ref scoreMod, middleName, 10);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				NickName = nickName,
				MiddleName = middleName,
				LastName = lastName,
				DisplayName = $"{firstName} {lastName}",
				Score = 125 + scoreMod
			};
			return pn;
		}
	}
}