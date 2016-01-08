using static BinaryFog.NameParser.PipeJoinedResources;

namespace BinaryFog.NameParser {
	public static class RegexNameComponents {
		public static readonly string JobTitle = @"(?<jobTitle>" + JobTitles + @")";
		public static readonly string Title = @"(?<title>(" + Titles + @")((?!\s)\W)?)";
		public static readonly string Suffix = @"(?<suffix>((" + Suffixes + @")((?!\s)\W)?)([\s]*(?<=[\s\W]+)(" + HonorificTitles + @")((?!\s)\W)?)*?|([\s]*(?<=[\s\W]+)(" + HonorificTitles + @")((?!\s)\W)?))";
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
