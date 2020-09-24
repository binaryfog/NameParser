namespace BinaryFog.NameParser {
    /// <summary>
    /// Implement this interface if you wish for your name parser pattern
    /// to be automatically discovered and used by the <see cref="FullNameParser"/>.
    /// 
    /// The implementing class must be public to be discovered automatically.
    /// 
    /// Feel free to submit your patterns upstream to the repository or create a separate pattern library.
    /// </summary>
    public interface IFullNamePattern
    {

        /// <summary>
        /// Attempt to parse a name through the pattern.
        /// </summary>
        /// <param name="rawName">A raw name.</param>
        /// <returns>A scored parsed result.</returns>
        ParsedFullName Parse(string rawName);

    }
}
