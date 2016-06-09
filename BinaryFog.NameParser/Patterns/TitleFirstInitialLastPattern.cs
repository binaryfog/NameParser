using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleFirstInitialLastPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + Initial + Space + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);


		public ParsedName Parse(string rawName) {
			//Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
            var pn = new ParsedName(this.GetType().Name)
            {
                Title = match.Groups["title"].Value,
				FirstName = match.Groups["first"].Value,
				LastName = match.Groups["last"].Value,
				DisplayName = $"{match.Groups["first"].Value} {match.Groups["last"].Value}",
				Score = 100
			};
			return pn;
		}
	}
}
