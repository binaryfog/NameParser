using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryFog.NameParser {
	public static class RegexNameComponents {

		private static string SplitAndJoin(string res) {
			var stringBuilder = new StringBuilder();
			using (var reader = new StringReader(res)) {
				stringBuilder.Append(reader.ReadLine());
				for(;;) {
					var line = reader.ReadLine();
					if (string.IsNullOrWhiteSpace(line)) break;
					stringBuilder.Append('|').Append(Regex.Escape(line));
				}
			}
			return stringBuilder.ToString();
		}
		
		//public static readonly string FemaleFirstNames = SplitAndJoin(Resources.FemaleFirstNames);
		//public static readonly string MaleFirstNames = SplitAndJoin(Resources.MaleFirstNames);
		//public static readonly string USCensusLastNames = SplitAndJoin(Resources.USCensusLastNames);
		public static readonly string LastNamePrefixes = SplitAndJoin(Resources.LastNamePrefixes);
		public static readonly string PostNominals = SplitAndJoin(Resources.PostNominals);
		public static readonly string JobTitles = SplitAndJoin(Resources.JobTitles);
		public static readonly string Suffixes = SplitAndJoin(Resources.Suffixes);
		public static readonly string Titles = SplitAndJoin(Resources.Titles);
		public static readonly string CompanySuffixes = SplitAndJoin(Resources.CompanySuffixes);


		public static readonly string JobTitle = @"(?<jobTitle>" + JobTitles + @")";
		public static readonly string Title = @"(?<title>(" + Titles + @")((?!\s)\W)?)";
		public static readonly string Suffix = @"(?<suffix>((" + Suffixes + @")((?!\s)\W)?)([\s]*(?<=[\s\W]+)(" + PostNominals + @")((?!\s)\W)?)*?|([\s]*(?<=[\s\W]+)(" + PostNominals + @")((?!\s)\W)?))";
		public static readonly string Prefix = @"(?<prefix>" + LastNamePrefixes + @")";
		public const string Space = @"((?<=\W)\s*|\s*(?=\W)|(?<!\W)\s+)";
		public const string OptionalCommaSpace = @"(\s*,)?\s*";
		public const string CommaSpace = @"\s*,\s*";
		public const string Initial = @"(?<initial>[a-z]\.?)";
		public const string First = @"(?<first>\w+)";
		public const string Last = @"(?<last>\w+)";
		public const string Middle = @"(?<middle>\w+)";
		public const string LastHyphenated = @"(?<last>(?<lastPart1>\w+)-(?<lastPart2>\w+))";
		public const string Nick = @"(?=\(\w+\)|'\w+'|""\w+"")[\('""](?<nick>\w+)[\)'""]";

		
	}
}
