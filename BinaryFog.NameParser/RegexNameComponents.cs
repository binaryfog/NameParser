using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryFog.NameParser
{
    using static Helpers;

    public static class RegexNameComponents
    {
        public const string Space = @"(\s+|(?<!\w)\s*)";
        public const string OptionalSpace = @"((?<=\W)\s*|\s*(?=\W)|(?<!\W)\s+)?";
        public const string OptionalCommaSpace = @"(" + OptionalSpace + @",)?" + Space;
        public const string CommaSpace = OptionalSpace + @"," + Space;
        public const string Initial = @"(?<initial>[a-z])\.?";
        public const string Vowels = @"[aeiouy]";

        public const string TwoInitial =
            @"(?<initial1>[a-z])(?!" + Vowels + @")\." + OptionalSpace + @"(?<initial2>[a-z])\.";

        public const string TwoInitialFirstOptionalDot =
            @"(?<initial1>[a-z])(?!" + Vowels + @")\.?" + OptionalSpace + @"(?<initial2>[a-z])\.";

        public const string TwoInitialSecondOptionalDot =
            @"(?<initial1>[a-z])(?!" + Vowels + @")\." + OptionalSpace + @"(?<initial2>[a-z])\.?";

        public const string Name = @"'?(\w+|\w+('\w+)+)('(?=\W))?";
        public const string First = @"(?<first>" + Name + @")";
        public const string AfricanFirst = @"(?<first>" + Name + "'" + Name + @")";
        public const string Last = @"(?<last>" + Name + @")";
        public const string Middle = @"(?<middle>" + Name + @")";
        public const string TwoMiddle = @"(?<middle1>" + Name + @")" + Space + @"(?<middle2>" + Name + @")";
        public const string Hyphen = "[-\u00AD\u058A\u1806\u2010\u2011\u30FB\uFE63\uFF0D\uFF65]";
        public const string HyphenOptionallySpaced = OptionalSpace + Hyphen + OptionalSpace;
        public const string TwoHyphenOptionallySpacedMiddle = @"(?<middle1>" + Name + @")" + HyphenOptionallySpaced + @"(?<middle2>" + Name + @")";

        public const string LastHyphenated =
            @"(?<last>(?<last1>" + Name + @")" + HyphenOptionallySpaced + @"(?<last2>" + Name + @"))";

        public const string SpaceOrHyphen = Space + "|" + HyphenOptionallySpaced;
        public const string Words = @"(\w+|" + SpaceOrHyphen + @")+";

        public const string Nick = @"(\((?<nick>(" + Words + @"))\)|(?<nickquote>['""”“])(?<nick>(" + Words +
                                   @"))(?<nickquote>['""”“]))";


        public const RegexOptions CommonPatternRegexOptions
            = RegexOptions.Compiled
              | RegexOptions.IgnoreCase
              | RegexOptions.Singleline
              | RegexOptions.CultureInvariant
              | RegexOptions.ExplicitCapture;

        public static readonly string LastNamePrefixes = RegexPipeJoin(Resources.LastNamePrefixes);
        public static readonly string PostNominals = RegexPipeJoin(Resources.PostNominals); // TODO: make use
        public static readonly string JobTitles = RegexPipeJoin(Resources.JobTitles);
        public static readonly string Suffixes = RegexPipeJoin(Resources.Suffixes);
        public static readonly string Titles = RegexPipeJoin(Resources.Titles);
        public static readonly string CompanySuffixes = RegexPipeJoin(Resources.CompanySuffixes);


        public static readonly string JobTitle = $@"(?<jobTitle>{JobTitles})";

        public static readonly string Title = $@"(?<title>({Titles})((?!\s)\W)?)";

        //public static readonly string Suffix = @"(?<suffix>((" + Suffixes + @")((?!\s)\W)?)([\s]*(?<=[\s\W]+)(" + PostNominals + @")((?!\s)\W)?)*?|([\s]*(?<=[\s\W]+)(" + PostNominals + @")((?!\s)\W)?))";

        public static readonly string Suffix = $@"(?<suffix>({Suffixes})((?!\s)\W)?)";
        public static readonly string Suffix1 = $@"(?<suffix1>({Suffixes})((?!\s)\W)?)";
        public static readonly string Suffix2 = $@"(?<suffix2>({Suffixes})((?!\s)\W)?)";

        public static readonly string Prefix = $@"(?<prefix>{LastNamePrefixes})";

        /// <summary>
        ///     Read and escape lines from a resource stream and combine
        ///     them all together with pipe characters to create a string
        ///     capable of being matched against when parsed as a <see cref="Regex" />.
        /// </summary>
        /// <param name="res">The resource stream containing strings as lines.</param>
        /// <returns>A <see cref="Regex" /> matchable concatenation of the resource.</returns>
        private static string RegexPipeJoin(Stream res)
        {
            // try to preallocate if stream length is known
            var resLength = checked((int) TryOrDefault(() => res!.Length));
            var stringBuilder = resLength != 0
                ? new StringBuilder(resLength)
                : new StringBuilder();
            using var reader = new StreamReader(res!);
            // first line case
            var line = reader.ReadLine();

            Debug.Assert(line != null);
            stringBuilder.Append(Regex.Escape(line));

            // second line
            line = reader.ReadLine();
            while (line != null)
            {
                stringBuilder.Append('|')
                    .Append(Regex.Escape(line));

                // remaining lines
                line = reader.ReadLine();
            }

            return stringBuilder.ToString();
        }
    }
}