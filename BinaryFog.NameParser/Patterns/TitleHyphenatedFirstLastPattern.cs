using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class TitleHyphenatedFirstLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + @"(?<first1>\w+)-(?<first2>\w+)" + Space + @"(?<last>\w+)$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;

			var firstName1 = match.Groups["first1"].Value;
            var firstName2 = match.Groups["first2"].Value;
			var lastName = match.Groups["last"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstName(ref scoreMod, firstName1);
			ModifyScoreExpectedFirstName(ref scoreMod, firstName2);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName
            {
                Title = match.Groups["title"].Value,
                FirstName = $"{firstName1}-{firstName2}",
				LastName = lastName,
				DisplayName = $"{firstName1}-{firstName2} {lastName}",
				Score = 200 + scoreMod
			};
			return pn;
		}
	}
}
