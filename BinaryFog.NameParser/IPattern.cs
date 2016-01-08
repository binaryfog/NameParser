namespace BinaryFog.NameParser {
	public interface IPattern {

		//string[] GetRegexPatterns();

		ParsedName Parse(string rawName);

	}
}
