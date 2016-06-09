using System.Text.RegularExpressions;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	internal class CompanyPattern : IPattern {
		private static readonly Regex Rx = new Regex(
			Space + @"(?<lastWord>(" + CompanySuffixes + @")\W?)$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);


		public ParsedName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
            var pn = new ParsedName(this.GetType().Name)
            {
                DisplayName = rawName,
				Score = ParsedName.MaxScore
			};

			return pn;
		}
	}
}
