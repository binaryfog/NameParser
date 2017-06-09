using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class FirstNameJobTitlePattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + JobTitle + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var jobTitle = match.Groups["jobTitle"].Value;
			var firstName = match.Groups["first"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName);

			var pn = new ParsedFullName {
				FirstName = firstName,

				DisplayName = $"{firstName} {jobTitle}",
				Score = 200 + scoreMod
			};
			return pn;
		}
	}
}
