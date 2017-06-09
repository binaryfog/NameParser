namespace BinaryFog.NameParser {
	/// <summary>
	/// Implement this interface if you wish for your name parser pattern
	/// to be automatically discovered and used by the <see cref="FullNameParser"/>.
	/// </summary>
	public interface IFullNamePattern {
		/// <summary>
		/// Attempt to parse a name through the pattern..
		/// </summary>
		/// <param name="rawName">A raw name.</param>
		/// <returns>A scored parsed result.</returns>
		ParsedFullName Parse(string rawName);

	}
}
