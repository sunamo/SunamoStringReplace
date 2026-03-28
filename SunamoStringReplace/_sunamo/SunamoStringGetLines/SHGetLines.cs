namespace SunamoStringReplace._sunamo.SunamoStringGetLines;

/// <summary>
/// Internal helper class for splitting text into lines.
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// Splits text into individual lines, handling all common newline formats.
    /// </summary>
    /// <param name="text">The text to split into lines.</param>
    /// <returns>A list of lines.</returns>
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    /// <summary>
    /// Further splits lines by individual Unix newline characters.
    /// </summary>
    /// <param name="lines">The list of lines to process.</param>
    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

    /// <summary>
    /// Splits lines that contain the specified delimiter into separate entries.
    /// </summary>
    /// <param name="lines">The list of lines to process.</param>
    /// <param name="delimiter">The delimiter to split by.</param>
    private static void SplitBy(List<string> lines, string delimiter)
    {
        for (var i = lines.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var carriageReturnNewline = lines[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturn = lines[i].Split(new[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewline.Length > 1)
                    ThrowEx.Custom("cannot contain any \r\name, pass already split by this pattern");
                else if (newlineCarriageReturn.Length > 1) ThrowEx.Custom("cannot contain any \n\r, pass already split by this pattern");
            }

            var splitResult = lines[i].Split(new[] { delimiter }, StringSplitOptions.None);

            if (splitResult.Length > 1) InsertOnIndex(lines, splitResult.ToList(), i);
        }
    }

    /// <summary>
    /// Replaces a single element in the list with multiple elements from a split result.
    /// </summary>
    /// <param name="lines">The list to modify.</param>
    /// <param name="splitResult">The split result to insert.</param>
    /// <param name="index">The index of the element to replace.</param>
    private static void InsertOnIndex(List<string> lines, List<string> splitResult, int index)
    {
        splitResult.Reverse();

        lines.RemoveAt(index);

        foreach (var element in splitResult) lines.Insert(index, element);
    }
}
