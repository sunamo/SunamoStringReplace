namespace SunamoStringReplace._sunamo;

/// <summary>
/// Internal string splitting helper class.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits a string by the specified delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of non-empty string parts.</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
