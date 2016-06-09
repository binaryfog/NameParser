using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	internal class FirstDoubleWordLastPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + @"(?<last1>\w+)" + Space + @"(?<last2>\w+)$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName = match.Groups["first"].Value;
			if (!FirstNames.Contains(firstName))
				return null;

            var pn = new ParsedName(this.GetType().Name)
            {
                FirstName = match.Groups["first"].Value,
				LastName = $"{match.Groups["last1"].Value} {match.Groups["last2"].Value}",
				DisplayName = $"{match.Groups["first"].Value} {match.Groups["last1"].Value} {match.Groups["last2"].Value}",
				Score = 100
			};
			return pn;
		}
	}
}
