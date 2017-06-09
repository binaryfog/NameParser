﻿using System.Text.RegularExpressions;
using JetBrains.Annotations;
using static BinaryFog.NameParser.RegexNameComponents;
using static BinaryFog.NameParser.NameComponentSets;

namespace BinaryFog.NameParser.Patterns {
	[UsedImplicitly]
	public class FirstMiddlePrefixedLastPattern : IFullNamePattern {
		private static readonly Regex Rx = new Regex(
			@"^" + First + Space + Middle + Space + Prefix + Space + Last + @"$",
			CommonPatternRegexOptions);


		public ParsedFullName Parse(string rawName) {
			var match = Rx.Match(rawName);
			if (!match.Success) return null;
			var prefix = match.Groups["prefix"].Value;
			var firstName = match.Groups["first"].Value;
			var middleName = match.Groups["middle"].Value;
			var lastName = match.Groups["last"].Value;
			
			var scoreMod = 0;
			ModifyScoreExpectedFirstNames(ref scoreMod, firstName, middleName);
			ModifyScoreExpectedLastName(ref scoreMod, lastName);

			var pn = new ParsedFullName {

				FirstName = firstName,
				MiddleName = middleName,

				LastName = prefix + " " + lastName,
				DisplayName = $"{firstName} {middleName} {prefix} {lastName}",
				Score = 250 + scoreMod
			};
			return pn;
		}
	}
}
