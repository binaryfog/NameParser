using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace BinaryFog.NameParser.Tests {
	public static class DataFiles {
		private static readonly string DirectoryPath
			= Path.Combine(
				Path.GetDirectoryName(
					new Uri(
						typeof(DataFiles).GetTypeInfo()
							.Assembly.CodeBase
					).LocalPath
				),
				"DataFiles"
			);

		public static Stream GetStream(string fileName)
			=> File.OpenRead(Path.Combine(DirectoryPath, fileName));

		private static ConcurrentDictionary<string, XDocument> XDocuments { get; }
			= new ConcurrentDictionary<string, XDocument>(StringComparer.OrdinalIgnoreCase);

		public static XDocument GetXDocument(string fileName) {
			if (!XDocuments.TryGetValue(fileName, out var doc))
				using (var stream = GetStream(fileName))
					XDocuments[fileName] = doc = XDocument.Load(stream);
			return doc;
		}
	}
}