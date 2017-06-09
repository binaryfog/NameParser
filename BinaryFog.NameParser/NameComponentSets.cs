using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryFog.NameParser {
	internal static class NameComponentSets {
		/// <summary>
		/// Reads lines from the stream as an enumeration of strings.
		/// </summary>
		/// <param name="res">The resource stream.</param>
		/// <returns>Lines extracted as strings from the stream.</returns>
		public static IEnumerable<string> ReadLines(Stream res) {
			using (var reader = new StreamReader(res)) {
				var line = reader.ReadLine();
				while (!string.IsNullOrWhiteSpace(line)) {
					yield return line;
					line = reader.ReadLine();
				}
			}
		}
		

		[SuppressMessage("ReSharper", "InconsistentNaming")]
		private static readonly ImmutableHashSet<string> _maleFirstNames =
			ReadLines(Resources.MaleFirstNames).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
		
		[SuppressMessage("ReSharper", "InconsistentNaming")]
		private static readonly ImmutableHashSet<string> _femaleFirstNames =
			ReadLines(Resources.FemaleFirstNames).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
		

		public static readonly ISet<string> MaleFirstNames = _maleFirstNames;
		public static readonly ISet<string> FemaleFirstNames = _femaleFirstNames;

		public static readonly ISet<string> FirstNames =
			_maleFirstNames.Union(_femaleFirstNames); 

		public static readonly ISet<string> LastNames =
			ReadLines(Resources.UsCensusLastNames).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);

		/// <summary>
		/// Apply adjustments to a score as a name is matched against first and last name dictionaries.
		/// </summary>
		/// <param name="score">The score to adjust.</param>
		/// <param name="name">The name to attempt to match.</param>
		/// <param name="matchedFirstValue">The value to adjust the score by if matched by the first name dictionary.</param>
		/// <param name="matchedLastValue">The value to adjust the score by if matched by the last name dictionary.</param>
		internal static void ModifyScore(ref int score, string name, int matchedFirstValue, int matchedLastValue) {
			if (FirstNames.Contains(name)) score += matchedFirstValue;
			if (LastNames.Contains(name)) score += matchedLastValue;
		}

		/// <summary>
		/// Apply a slight adjustment to a score if a name is matched against any name dictionary.
		/// </summary>
		/// <param name="score">The score to adjust.</param>
		/// <param name="name">The name to attempt to match.</param>
		/// <param name="value">The value to adjust the score by if matched.</param>
		internal static void ModifyScoreExpectedName(ref int score, string name, int value = 10)
			=> ModifyScore(ref score, name, value, value);
		
		/// <summary>
		/// Apply an adjustment to a score if a name is matched against the first name dictionary.
		/// An opposite adjustment is applied if the name is matched in the last name dictionary.
		/// </summary>
		/// <param name="score">The score to adjust.</param>
		/// <param name="name">The name to attempt to match.</param>
		/// <param name="value">The value to adjust the score by if matched.</param>
		internal static void ModifyScoreExpectedFirstName(ref int score, string name, int value = 25)
			=> ModifyScore(ref score, name, value, -value);
		
		/// <summary>
		/// Apply an adjustment to a score if a name is matched against the last name dictionary.
		/// An opposite adjustment is applied if the name is matched in the first name dictionary.
		/// </summary>
		/// <param name="score">The score to adjust.</param>
		/// <param name="name">The name to attempt to match.</param>
		/// <param name="value">The value to adjust the score by if matched.</param>
		internal static void ModifyScoreExpectedLastName(ref int score, string name, int value = 25)
			=> ModifyScore(ref score, name, -value, value);
	}

}