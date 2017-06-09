using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryFog.NameParser {
	using static Helpers;

	public static class RegexNameComponents {
		/// <summary>
		/// Read and escape lines from a resource stream and combine
		/// them all together with pipe characters to create a string
		/// capable of being matched against when parsed as a <see cref="Regex"/>.
		/// </summary>
		/// <param name="res">The resource stream containing strings as lines.</param>
		/// <returns>A <see cref="Regex"/> matchable concatenation of the resource.</returns>
		private static string RegexPipeJoin(Stream res) {
			// try to preallocate if stream length is known
			var resLength = checked((int) TryOrDefault(() => res.Length));
			var stringBuilder = resLength != 0
				? new StringBuilder(resLength)
				: new StringBuilder();
			using (var reader = new StreamReader(res)) {
				// first line case
				var line = reader.ReadLine();
				//if (line == null) return "";
				Debug.Assert(line != null);
				stringBuilder.Append(Regex.Escape(line));

				// second line
				line = reader.ReadLine();
				while (line != null) {
					stringBuilder.Append('|')
						.Append(Regex.Escape(line));

					// remaining lines
					line = reader.ReadLine();
				}
			}
			return stringBuilder.ToString();
		}

		public static readonly string LastNamePrefixes = RegexPipeJoin(Resources.LastNamePrefixes);
		public static readonly string PostNominals = RegexPipeJoin(Resources.PostNominals);
		public static readonly string JobTitles = RegexPipeJoin(Resources.JobTitles);
		public static readonly string Suffixes = RegexPipeJoin(Resources.Suffixes);
		public static readonly string Titles = RegexPipeJoin(Resources.Titles);
		public static readonly string CompanySuffixes = RegexPipeJoin(Resources.CompanySuffixes);


		public static readonly string JobTitle = @"(?<jobTitle>" + JobTitles + @")";

		public static readonly string Title = @"(?<title>(" + Titles + @")((?!\s)\W)?)";

		//public static readonly string Suffix = @"(?<suffix>((" + Suffixes + @")((?!\s)\W)?)([\s]*(?<=[\s\W]+)(" + PostNominals + @")((?!\s)\W)?)*?|([\s]*(?<=[\s\W]+)(" + PostNominals + @")((?!\s)\W)?))";

		public static readonly string Suffix = @"(?<suffix>(" + Suffixes + @")((?!\s)\W)?)";

		public static readonly string Prefix = @"(?<prefix>" + LastNamePrefixes + @")";

		public const string Space = @"((?<=\W)\s*|\s*(?=\W)|(?<!\W)\s+)";
		public const string OptionalSpace = @"((?<=\W)\s*|\s*(?=\W)|(?<!\W)\s+)?";
		public const string OptionalCommaSpace = @"(" + OptionalSpace + @",)?" + Space;
		public const string CommaSpace = @"\s*,\s*";
		public const string Initial = @"(?<initial>[a-z]\.?)";
		public const string First = @"(?<first>\w+|\w+'\w*)";
		public const string Last = @"(?<last>\w+|\w+'\w*|'\w+)";
		public const string Middle = @"(?<middle>\w+)";
		public const string TwoMiddle = @"(?<middle1>\w+)" + Space + @"(?<middle2>\w+)";
		public const string Hyphen = "[-\u00AD\u058A\u1806\u2010\u2011\u30FB\uFE63\uFF0D\uFF65]";
		public const string LastHyphenated = @"(?<last>(?<lastPart1>\w+)" + Hyphen + @"(?<lastPart2>\w+))";
		public const string Nick = @"(?=\(\w+\)|'\w+'|""\w+"")[\('""](?<nick>\w+)[\)'""]";
	}
}