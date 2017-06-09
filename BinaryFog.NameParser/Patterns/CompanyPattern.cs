using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class CompanyPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			Space + @"(?<lastWord>(" + CompanySuffixes + @")\W?)$",
			CommonPatternRegexOptions);


		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var pn = new ParsedFullName {
				DisplayName = rawName,
				Score = ParsedFullName.MaxScore
			};

			return pn;
		}
	}
}
