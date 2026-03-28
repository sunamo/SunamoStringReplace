namespace SunamoStringReplace;

/// <summary>
/// Provides methods for replacing content within strings.
/// </summary>
public partial class SHReplace
{
    private static readonly StringBuilder stringBuilder = new();

    /// <summary>
    /// Replaces all double spaces with single spaces in the given text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="isAlsoReplacingHtml">Whether to also replace HTML non-breaking spaces.</param>
    /// <returns>The text with all double spaces replaced by single spaces.</returns>
    public static string ReplaceAllDoubleSpaceToSingle(string text, bool isAlsoReplacingHtml = false)
    {
        if (isAlsoReplacingHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        while (text.Contains("  "))
            text = ReplaceAll2(text, " ", "  ");
        return text;
    }

    /// <summary>
    /// Splits a string using advanced options such as replacing newlines, trimming, and escaping quotations.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="isReplacingNewLineBySpace">Whether to replace newline characters with spaces.</param>
    /// <param name="isUsingMoreSpacesForOne">Whether to collapse multiple spaces into one.</param>
    /// <param name="isTrimming">Whether to trim each resulting element.</param>
    /// <param name="isEscapingQuotations">Whether to escape quotation marks.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of split string elements.</returns>
    public static List<string> SplitAdvanced(string text, bool isReplacingNewLineBySpace, bool isUsingMoreSpacesForOne, bool isTrimming, bool isEscapingQuotations, params string[] delimiters)
    {
        var parts = text.Split(delimiters, StringSplitOptions.None).ToList();
        if (isReplacingNewLineBySpace)
            for (var i = 0; i < parts.Count; i++)
                parts[i] = ReplaceAll(parts[i], "", "\r", @"\n", Environment.NewLine);
        if (isUsingMoreSpacesForOne)
            for (var i = 0; i < parts.Count; i++)
                parts[i] = ReplaceAll2(parts[i], " ", "");
        if (isTrimming)
            parts = parts.ConvertAll(element => element.Trim());
        if (isEscapingQuotations)
        {
            var replacement = "\"";
            for (var i = 0; i < parts.Count; i++)
                parts[i] = ReplaceFromEnd(parts[i], "\"", replacement);
        }

        return parts;
    }

    /// <summary>
    /// Replaces all double spaces with single spaces in the given text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <returns>The text with all double spaces replaced by single spaces.</returns>
    public static string ReplaceAllDoubleSpaceToSingle(string text)
    {
        return ReplaceAllDoubleSpaceToSingle(text, false);
    }

    /// <summary>
    /// Replaces occurrences of a search string in the text passed by reference.
    /// </summary>
    /// <param name="text">The text to modify, passed by reference.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <returns>The modified text.</returns>
    public static string ReplaceRef(ref string text, string what, string replacement)
    {
        text = text.Replace(what, replacement);
        return text;
    }

    /// <summary>
    /// Replaces all occurrences of a search string from the end of the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to search for.</param>
    /// <returns>The text with replacements applied from end to start.</returns>
    public static string ReplaceFromEnd(string text, string replacement, string what)
    {
        var occurrences = SH.ReturnOccurencesOfString(text, what);
        for (var i = occurrences.Count - 1; i >= 0; i--)
            text = ReplaceByIndex(text, replacement, occurrences[i], what.Length);
        return text;
    }

    /// <summary>
    /// Replaces all whitespace characters in the text with the specified replacement.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace whitespace characters with.</param>
    /// <returns>The text with all whitespace characters replaced.</returns>
    public static string ReplaceWhitespaces(string text, string replacement)
    {
        WhitespaceCharService whitespaceCharService = new WhitespaceCharService();
        foreach (var character in whitespaceCharService.WhiteSpaceChars)
            text = text.Replace(character.ToString(), replacement);
        return text;
    }

    /// <summary>
    /// Replaces whitespace characters in the text and trims the result.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <returns>The text with whitespace characters replaced and trimmed.</returns>
    public static string ReplaceWhiteSpacesAndTrim(string text)
    {
        return ReplaceWhiteSpaces(text).Trim();
    }

    /// <summary>
    /// Replaces whitespace characters (excluding spaces) with the specified replacement.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace whitespace characters with.</param>
    /// <returns>The text with whitespace characters (except spaces) replaced.</returns>
    public static string ReplaceWhiteSpacesWithoutSpaces(string text, string replacement)
    {
        return ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(text, replacement);
    }

    /// <summary>
    /// Replaces carriage return, newline, and tab characters with the specified replacement.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace whitespace characters with.</param>
    /// <returns>The text with carriage return, newline, and tab characters replaced.</returns>
    public static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string text, string replacement)
    {
        return text.Replace("\r", replacement).Replace("\n", replacement).Replace("\t", replacement);
    }

    /// <summary>
    /// Replaces template variables in HTML content with values from a data binding table.
    /// </summary>
    /// <param name="openChar">The character that opens a variable reference.</param>
    /// <param name="closeChar">The character that closes a variable reference.</param>
    /// <param name="innerHtml">The HTML content containing variable references.</param>
    /// <param name="dataBinding">The data binding table with variable values.</param>
    /// <param name="actualRow">The row index to use for variable values.</param>
    /// <returns>The HTML content with variables replaced by their values.</returns>
    public static string ReplaceVariables(char openChar, char closeChar, string innerHtml, List<List<string>> dataBinding, int actualRow)
    {
        var unparsedBuilder = new StringBuilder();
        var parsedBuilder = new StringBuilder();
        var isInVariable = false;
        if (innerHtml != null)
            foreach (var character in innerHtml)
                if (character == openChar)
                {
                    isInVariable = true;
                }
                else if (character == closeChar)
                {
                    if (isInVariable)
                        isInVariable = false;
                    var parsedIndex = 0;
                    if (int.TryParse(unparsedBuilder.ToString(), out parsedIndex))
                    {
                        var cellValue = dataBinding[parsedIndex][actualRow];
                        parsedBuilder.Append(cellValue);
                    }
                    else
                    {
                        parsedBuilder.Append(openChar + unparsedBuilder.ToString() + closeChar);
                    }

                    unparsedBuilder.Clear();
                }
                else if (isInVariable)
                {
                    unparsedBuilder.Append(character);
                }
                else
                {
                    parsedBuilder.Append(character);
                }

        return parsedBuilder.ToString();
    }

    /// <summary>
    /// Replaces a portion of text at a specific index with the specified replacement.
    /// </summary>
    /// <param name="text">The text to modify.</param>
    /// <param name="replacement">The string to insert.</param>
    /// <param name="index">The starting index of the portion to replace.</param>
    /// <param name="length">The length of the portion to replace.</param>
    /// <returns>The modified text.</returns>
    public static string ReplaceByIndex(string text, string replacement, int index, int length)
    {
        text = text.Remove(index, length);
        if (replacement != string.Empty)
            text = text.Insert(index, replacement);
        return text;
    }

    /// <summary>
    /// Replaces a portion of a StringBuilder at a specific index with the specified replacement.
    /// </summary>
    /// <param name="builder">The StringBuilder to modify.</param>
    /// <param name="replacement">The string to insert.</param>
    /// <param name="index">The starting index of the portion to replace.</param>
    /// <param name="length">The length of the portion to replace.</param>
    /// <returns>The modified StringBuilder.</returns>
    public static StringBuilder ReplaceByIndex(StringBuilder builder, string replacement, int index, int length)
    {
        builder = builder.Remove(index, length);
        if (replacement != string.Empty)
            builder = builder.Insert(index, replacement);
        return builder;
    }

    /// <summary>
    /// Replaces all occurrences of a search string with a replacement, optionally treating input as paired lines.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="isPairLines">Whether to treat input as paired lines for replacement.</param>
    /// <returns>The text with replacements applied.</returns>
    public static string ReplaceAll2(string text, string replacement, string what, bool isPairLines)
    {
        if (isPairLines)
        {
            var fromList = SHSplit.Split(what, Environment.NewLine);
            var toList = SHSplit.Split(replacement, Environment.NewLine);
            ThrowEx.DifferentCountInLists("fromList", fromList, "toList", toList);
            for (var i = 0; i < fromList.Count; i++)
                text = ReplaceAll2(text, toList[i], fromList[i]);
            return text;
        }

        return ReplaceAll2(text, replacement, what);
    }

    /// <summary>
    /// Replaces all occurrences of multiple search strings in a StringBuilder with a single replacement.
    /// </summary>
    /// <param name="builder">The StringBuilder to modify.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="searchValues">The strings to search for and replace.</param>
    /// <returns>The modified StringBuilder.</returns>
    public static StringBuilder ReplaceAllSb(StringBuilder builder, string replacement, params string[] searchValues)
    {
        foreach (var oldValue in searchValues)
        {
            if (oldValue == replacement)
                continue;
            builder = builder.Replace(oldValue, replacement);
        }

        return builder;
    }

    /// <summary>
    /// Replaces content in the input text using a multiline mapping string with arrow notation.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="mappingText">The mapping text with lines in format "from->to".</param>
    /// <param name="isRemovingEndingPairCharsWhenDontHaveStarting">Whether to remove unmatched ending pair characters.</param>
    /// <returns>The text with all mapped replacements applied.</returns>
    public static string ReplaceMany(string text, string mappingText, bool isRemovingEndingPairCharsWhenDontHaveStarting = true)
    {
        var fromBuilder = new StringBuilder();
        var toBuilder = new StringBuilder();
        var lines = SHGetLines.GetLines(mappingText);
        lines = lines.Where(line => line.Trim() != string.Empty).ToList();
        var delimiter = "->";
        var replaceForEmpty = new List<string>();
        foreach (var mappingLine in lines)
        {
            var parts = SHSplit.Split(mappingLine, delimiter);
            if (parts.Count == 1)
                if (mappingLine.EndsWith(delimiter))
                {
                    replaceForEmpty.Add(parts[0]);
                    continue;
                }

            fromBuilder.AppendLine(parts[0]);
            toBuilder.AppendLine(parts[1]);
        }

        var result = ReplaceAll2(text, toBuilder.ToString(), fromBuilder.ToString(), true);
        foreach (var oldValue in replaceForEmpty)
            result = result.Replace(oldValue, string.Empty);
        if (isRemovingEndingPairCharsWhenDontHaveStarting)
            result = SH.RemoveEndingPairCharsWhenDontHaveStarting(result, "{", "}");
        return result;
    }

    /// <summary>
    /// Replaces all whitespace characters with a space character.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <returns>The text with all whitespace characters replaced by spaces.</returns>
    public static string ReplaceAllWhitecharsForSpace(string text)
    {
        WhitespaceCharService whitespaceCharService = new WhitespaceCharService();
        foreach (var character in whitespaceCharService.WhiteSpaceChars)
            if (character != ' ')
                text = text.Replace(character, ' ');
        return text;
    }
}
