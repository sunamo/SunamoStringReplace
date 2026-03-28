namespace SunamoStringReplace;

/// <summary>
/// Provides additional methods for replacing content within strings (partial class).
/// </summary>
public partial class SHReplace
{
    /// <summary>
    /// Replaces the first line of the text if it matches the expected value.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The expected first line value.</param>
    /// <param name="replacement">The string to replace the first line with.</param>
    /// <returns>The text with the first line replaced.</returns>
    public static string ReplaceFirstLine(string text, string what, string replacement)
    {
        var lines = SHGetLines.GetLines(text);
        if (lines[0] == what)
            lines[0] = replacement;
        else
            throw new Exception($"First line is not excepted '{what}', was '{lines[0]}'");
        return string.Join("\n", lines);
    }

    /// <summary>
    /// Replaces all occurrences of a search string in a loop until none remain.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to search for.</param>
    /// <returns>The text with all occurrences replaced.</returns>
    public static string ReplaceAll4(string text, string replacement, string what)
    {
        while (text.Contains(what))
            text = text.Replace(what, replacement);
        return text;
    }

    /// <summary>
    /// Replaces multiple search strings with corresponding replacement strings, optionally handling multiline content with various indentation.
    /// </summary>
    /// <param name="replaceFrom">The list of strings to search for.</param>
    /// <param name="replaceTo">The list of replacement strings.</param>
    /// <param name="isMultilineWithVariousIndent">Whether to handle multiline content with different indentation levels.</param>
    /// <param name="content">The content to process.</param>
    /// <returns>The content with all replacements applied.</returns>
    public static string ReplaceAll3(IList<string> replaceFrom, IList<string> replaceTo, bool isMultilineWithVariousIndent, string content)
    {
        WhitespaceCharService whitespaceCharService = new WhitespaceCharService();
        if (isMultilineWithVariousIndent)
            for (var i = 0; i < replaceFrom.Count; i++)
            {
                var replaceFromWithoutEmptyElements = replaceFrom[i].Split(whitespaceCharService.WhiteSpaceChars.ToArray()).ToList();
                var contentWithoutEmptyElements = content.Split(whitespaceCharService.WhiteSpaceChars.ToArray()).ToList();
                var equalRanges = CAG.EqualRanges(contentWithoutEmptyElements, replaceFromWithoutEmptyElements);
                if (equalRanges.Count == 0)
                    return content;
                var startIndex = (int)equalRanges.First().FromL;
                var endIndex = (int)equalRanges.Last().ToL;
                var mappedIndexes = new List<int>(contentWithoutEmptyElements.Count());
                var searchFrom = 0;
                foreach (var element in contentWithoutEmptyElements)
                {
                    var foundIndex = content.IndexOf(element, searchFrom);
                    searchFrom = foundIndex + element.Length;
                    mappedIndexes.Add(foundIndex);
                }

                var additionalLength = contentWithoutEmptyElements[endIndex].Length;
                startIndex = mappedIndexes[startIndex];
                endIndex = mappedIndexes[endIndex];
                endIndex += additionalLength;
                var whatToReplace = content.Substring(startIndex, endIndex - startIndex);
                content = content.Replace(whatToReplace, replaceTo[i]);
            }
        else
            for (var i = 0; i < replaceFrom.Count; i++)
                content = content.Replace(replaceFrom[i], replaceTo[i]);
        return content;
    }

    /// <summary>
    /// Replaces the first occurrence of a search string at a tracked index position.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="foundIndex">Reference to the index where the replacement occurred; -1 if not yet found.</param>
    /// <returns>The text with the replacement applied.</returns>
    public static string ReplaceWithIndex(string text, string what, string replacement, ref int foundIndex)
    {
        if (foundIndex == -1)
        {
            foundIndex = text.IndexOf(what);
            if (foundIndex != -1)
            {
                text = text.Remove(foundIndex, what.Length);
                text = text.Insert(foundIndex, replacement);
            }
        }

        return text;
    }

    /// <summary>
    /// Replaces typed whitespace escape sequences with their actual character equivalents.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="isReplacingQuotes">Whether to replace escaped quotes.</param>
    /// <param name="isReplacingT24">Whether to replace the \\t24 escape sequence.</param>
    /// <param name="isReplacingBackslash">Whether to replace escaped backslashes.</param>
    /// <returns>The text with escape sequences replaced by actual characters.</returns>
    public static string ReplaceTypedWhitespacesForNormal(string text, bool isReplacingQuotes, bool isReplacingT24, bool isReplacingBackslash)
    {
        stringBuilder.Clear();
        text = text.Trim();
        text = text.TrimEnd('"', '\'');
        stringBuilder.Append(text);
        return ReplaceTypedWhitespacesForNormal(stringBuilder, isReplacingQuotes, isReplacingT24, isReplacingBackslash).ToString();
    }

    /// <summary>
    /// Replaces typed whitespace escape sequences with their actual character equivalents in a StringBuilder.
    /// </summary>
    /// <param name="builder">The StringBuilder to process.</param>
    /// <param name="isReplacingQuotes">Whether to replace escaped quotes.</param>
    /// <param name="isReplacingT24">Whether to replace the \\t24 escape sequence.</param>
    /// <param name="isReplacingBackslash">Whether to replace escaped backslashes.</param>
    /// <returns>The StringBuilder with escape sequences replaced by actual characters.</returns>
    public static StringBuilder ReplaceTypedWhitespacesForNormal(StringBuilder builder, bool isReplacingQuotes, bool isReplacingT24, bool isReplacingBackslash)
    {
        if (isReplacingT24)
            builder = builder.Replace("\\\\t24", string.Empty);
        builder = builder.Replace("\\t", "\t");
        builder = builder.Replace("\\n", "\n");
        builder = builder.Replace("\\r", "\r");
        if (isReplacingQuotes)
            builder = builder.Replace("\\\"", "\"");
        if (isReplacingBackslash)
            builder = builder.Replace("\\\\", "\\");
        return builder;
    }

    /// <summary>
    /// Replaces a search string in a single line of text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="isCheckingForMoreOccurences">Whether to check for multiple occurrences.</param>
    /// <returns>The text with the replacement applied.</returns>
    public static string ReplaceInLine(string text, string what, string replacement, bool isCheckingForMoreOccurences)
    {
        var list = new List<string>(new[] { text });
        ReplaceInLine(list, 1, what, replacement, isCheckingForMoreOccurences);
        return list[0];
    }

    /// <summary>
    /// Replaces a search string in a specific line (1-based) within a list of lines.
    /// </summary>
    /// <param name="lines">The list of lines to modify.</param>
    /// <param name="lineFromOne">The 1-based line number to modify.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="isCheckingForMoreOccurences">Whether to check for multiple occurrences.</param>
    public static void ReplaceInLine(List<string> lines, int lineFromOne, string what, string replacement, bool isCheckingForMoreOccurences)
    {
        if (isCheckingForMoreOccurences)
        {
            var occurrences = SH.ReturnOccurencesOfString(lines[lineFromOne - 1], what);
            if (occurrences.Count > 1)
                foreach (var occurrence in occurrences)
                {
                    var afterChar = lines[lineFromOne - 1][occurrence + what.Length];
                    if (afterChar == ',' || afterChar == ' ')
                    {
                        var lineText = lines[lineFromOne - 1];
                        lineText = lineText.Remove(occurrence, what.Length);
                        lineText = lineText.Insert(occurrence, replacement);
                        lines[lineFromOne - 1] = lineText;
                        break;
                    }
                }
            else
                lines[lineFromOne - 1] = ReplaceOnce(lines[lineFromOne - 1], what, replacement);
        }
        else
        {
            lines[lineFromOne - 1] = ReplaceOnce(lines[lineFromOne - 1], what, replacement);
        }
    }

    /// <summary>
    /// Replaces only the first occurrence of a search string in the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <returns>The text with the first occurrence replaced.</returns>
    public static string ReplaceOnce(string text, string what, string replacement)
    {
        if (what == "")
            return text;
        var position = text.IndexOf(what);
        if (position == -1)
            return text;
        return text.Substring(0, position) + replacement + text.Substring(position + what.Length);
    }
}
