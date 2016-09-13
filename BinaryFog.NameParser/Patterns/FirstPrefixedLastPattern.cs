using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class FirstPrefixedLastPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + " " + Prefix + Space + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
            var pn = new ParsedName(this.GetType().Name)
            {
                FirstName = match.Groups["first"].Value,

				LastName = prefix + " " + match.Groups["last"].Value,
				DisplayName = $"{match.Groups["first"].Value} {prefix} {match.Groups["last"].Value}",
				Score = 275
			};
			return pn;
		}
	}
}
