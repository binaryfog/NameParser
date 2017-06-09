using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class SingleNameOnlyPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + @"$",
			CommonPatternRegexOptions);


		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var pn = new ParsedFullName {
				DisplayName = rawName,
				Score = 50
			};

			var matchedName = match.Groups["first"].Value;

			if (LastNames.Contains(matchedName))
				pn.LastName = matchedName;
			else
				pn.FirstName = matchedName;

			return pn;
		}
	}
}
