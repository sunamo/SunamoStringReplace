namespace SunamoStringReplace._sunamo;

/// <summary>
/// Provides a list of whitespace character codes and their corresponding characters.
/// </summary>
internal class WhitespaceCharService
{
    /// <summary>
    /// List of whitespace characters derived from <see cref="WhiteSpacesCodes"/>.
    /// </summary>
    internal List<char> WhiteSpaceChars;

    /// <summary>
    /// List of Unicode code points representing whitespace characters.
    /// </summary>
    internal readonly List<int> WhiteSpacesCodes = new(new[]
    {
        9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
        8232, 8233, 8239, 8287, 12288
    });

    /// <summary>
    /// Initializes a new instance and converts whitespace codes to characters.
    /// </summary>
    internal WhitespaceCharService()
    {
        WhiteSpaceChars = WhiteSpacesCodes.ConvertAll(code => (char)code);
    }
}
