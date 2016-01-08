using System.IO;
using System.Text;

namespace BinaryFog.NameParser {
	public static class PipeJoinedResources {

		private static string SplitAndJoin(string res) {
			var stringBuilder = new StringBuilder();
			using (var reader = new StringReader(res)) {
				stringBuilder.Append(reader.ReadLine());
				for(;;) {
					var line = reader.ReadLine();
					if (line == null) break;
					stringBuilder.Append('|').Append(line);
				}
			}
			return stringBuilder.ToString();
		}
		
		//public static string FemaleFirstNames = SplitAndJoin(Resources.FemaleFirstNames);
		//public static string MaleFirstNames = SplitAndJoin(Resources.MaleFirstNames);
		//public static string USCensusLastNames = SplitAndJoin(Resources.USCensusLastNames);
		public static string LastNamePrefixes = SplitAndJoin(Resources.LastNamePrefixes);
		public static string HonorificTitles = SplitAndJoin(Resources.HonorificTitles);
		public static string JobTitles = SplitAndJoin(Resources.JobTitles);
		public static string Suffixes = SplitAndJoin(Resources.Suffixes);
		public static string Titles = SplitAndJoin(Resources.Titles);
		public static string CompanySuffixes = SplitAndJoin(Resources.CompanySuffixes);
	}
}
