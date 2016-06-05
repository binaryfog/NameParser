using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleFirstLastSuffixPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + Last + OptionalCommaSpace + Suffix + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedName Parse(string rawName) {
			//Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
			//Suffix should be I or II or III or Jr. or Jr or Sr. or Sr or ESQ or ESQ. or ESQ"
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var pn = new ParsedName {
				Title = match.Groups["title"].Value,
				FirstName = match.Groups["first"].Value,
				LastName = match.Groups["last"].Value,
				DisplayName = $"{match.Groups["first"].Value} {match.Groups["last"].Value}",
				Suffix = match.Groups["suffix"].Value,
				Score = 100
			};
			return pn;
		}
	}
}
