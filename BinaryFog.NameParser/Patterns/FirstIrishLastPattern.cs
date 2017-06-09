using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class FirstIrishLastPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + "O'" + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var pn = new ParsedName(this.GetType().Name) {
                FirstName = match.Groups["first"].Value,
				LastName = $"O'{match.Groups["last"].Value}",
				DisplayName = $"{match.Groups["first"].Value} O'{match.Groups["last"].Value}",
				Score = 300
			};
			return pn;
		}
	}
}
