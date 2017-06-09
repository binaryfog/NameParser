using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstNickInitialLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + OptionalSpace + Nick + OptionalSpace + Initial + Space + Last + @"$",
			CommonPatternRegexOptions);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			var middleName = match.Groups["initial"].Value;
			var lastName = match.Groups["last"].Value;
			var nickName = match.Groups["nick"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedName(ref scoreMod, nickName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				FirstName = firstName,
				MiddleName = $"{middleName}.",
				LastName = lastName,
				NickName = nickName,
				DisplayName = $"{firstName} {middleName}. {lastName}",
				Score = 200 + scoreMod
			};
			return pn;
		}
	}
}
