using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class TitleFirstMiddlePrefixedLastSuffixPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + Middle + Space + Prefix + Space + Last + OptionalCommaSpace + Suffix + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
            var pn = new ParsedName(this.GetType().Name)
            {
                Title = match.Groups["title"].Value,
				FirstName = match.Groups["first"].Value,
				MiddleName = match.Groups["middle"].Value,
				LastName = prefix + " " + match.Groups["last"].Value,
				Suffix = match.Groups["suffix"].Value,
				DisplayName = $"{match.Groups["first"].Value} {prefix} {match.Groups["last"].Value}",
				Score = 300
			};
			return pn;
		}
	}
}
