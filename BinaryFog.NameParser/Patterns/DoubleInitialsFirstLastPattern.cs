using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	internal class DoubleInitialsFirstLastPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + @"(?<first1>\w\.)" + OptionalSpace + @"(?<first2>\w\.?)" + Space + @"(?<last>\w+)$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var firstName1 = match.Groups["first1"].Value;
            var firstName2 = match.Groups["first2"].Value;

            var pn = new ParsedName(this.GetType().Name)
            {
                FirstName = $"{match.Groups["first1"].Value}{match.Groups["first2"].Value}",
				LastName = match.Groups["last"].Value,
				DisplayName = $"{match.Groups["first1"].Value}{match.Groups["first2"].Value} {match.Groups["last"].Value}",
				Score = 100
			};
			return pn;
		}
	}
}
