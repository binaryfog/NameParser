using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	internal class TitleFirstIrishLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + Title + Space + First + Space + "O'" + Last + @"$",
			RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public ParsedFullName Parse(string rawName) {
			//Title should be Mr or Mr. or Ms or Ms. or Mrs or Mrs.
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
            var pn = new ParsedFullName()
            {
                Title = match.Groups["title"].Value,
				FirstName = match.Groups["first"].Value,
                LastName = $"O'{match.Groups["last"].Value}",
                DisplayName = $"{match.Groups["first"].Value} O'{match.Groups["last"].Value}",
                Score = 300
			};

			return pn;
		}
	}
}
