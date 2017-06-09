using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstTwoLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + @"(?<last1>\w+)" + Space + @"(?<last2>\w+)$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;

			var lastName1 = match.Groups["last1"].Value;
			var lastName2 = match.Groups["last2"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName1);
			ModifyScoreExpectedLastName(ref scoreMod, lastName2);

			var pn = new ParsedFullName {
				FirstName = firstName,
				LastName = $"{lastName1} {lastName2}",
				DisplayName = $"{firstName} {lastName1} {lastName2}",
				Score = 50 + scoreMod
			};
			return pn;
		}
	}
}
