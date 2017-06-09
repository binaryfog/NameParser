using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFog.NameParser {
	public static class Resources {
		private static Type Type { get; } = typeof(Resources);

		public static string ResourceNamespace { get; } = Type.FullName;
		public static Assembly Assembly { get; } = Type.GetTypeInfo().Assembly;

		private static Stream GetTxtStream(string name) {
			return Assembly.GetManifestResourceStream($"{ResourceNamespace}.{name}.txt");
		}

		public static Stream CompanySuffixes { get; }
			= GetTxtStream(nameof(CompanySuffixes));

		public static Stream FemaleFirstNames { get; }
			= GetTxtStream(nameof(FemaleFirstNames));

		public static Stream JobTitles { get; }
			= GetTxtStream(nameof(JobTitles));

		public static Stream LastNamePrefixes { get; }
			= GetTxtStream(nameof(LastNamePrefixes));

		public static Stream MaleFirstNames { get; }
			= GetTxtStream(nameof(MaleFirstNames));

		public static Stream PostNominals { get; }
			= GetTxtStream(nameof(PostNominals));

		public static Stream Suffixes { get; }
			= GetTxtStream(nameof(Suffixes));

		public static Stream Titles { get; }
			= GetTxtStream(nameof(Titles));
		
		public static Stream UsCensusLastNames { get; }
			= GetTxtStream(nameof(UsCensusLastNames));

	}
}