using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class TitleFirstDoubleWordHyphenatedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + @"(?<last1>\w+)-(?<last2>\w+)$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
            var firstName = match.Groups["first"].Value;
            if (!FirstNames.Contains(firstName.ToLowerInvariant()))
                return null;

            var pn = new ParsedFullName()
            {
                Title = match.Groups["title"].Value,
                FirstName = match.Groups["first"].Value,
                LastName = $"{match.Groups["last1"].Value}-{match.Groups["last2"].Value}",
                DisplayName = $"{match.Groups["first"].Value} {match.Groups["last1"].Value}-{match.Groups["last2"].Value}",
                Score = 200
			};
			return pn;
		}
	}
}
