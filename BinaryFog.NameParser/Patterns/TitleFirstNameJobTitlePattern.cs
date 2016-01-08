using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleFirstNameJobTitlePattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + JobTitle + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);


		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var jobTitle = match.Groups["jobTitle"].Value;
			var pn = new ParsedName {
				Title = match.Groups["title"].Value,
				FirstName = match.Groups["first"].Value,

				DisplayName = $"{match.Groups["first"].Value} {jobTitle}",
				Score = 200
			};
			return pn;
		}
	}
}
