using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class FirstMiddlePrefixedLastPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Middle + Space + Prefix + Space + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);


		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
			var pn = new ParsedName {

				FirstName = match.Groups["first"].Value,
				MiddleName = match.Groups["middle"].Value,

				LastName = prefix + " " + match.Groups["last"].Value,
				DisplayName = $"{match.Groups["first"].Value} {prefix} {match.Groups["last"].Value}",
				Score = 250
			};
			return pn;
		}
	}
}
