using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class TitleFirstInitialLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + Initial + Space + Last + @"$",
			CommonPatternRegexOptions);


		public ParsedFullName Parse(string rawName) {
			//Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			var middleName = $"{match.Groups["initial"].Value}.";
			var lastName = match.Groups["last"].Value;

			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {
				Title = match.Groups["title"].Value,
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				DisplayName = $"{firstName} {middleName} {lastName}",
				Score = 100 + scoreMod
			};
			return pn;
		}
	}
}