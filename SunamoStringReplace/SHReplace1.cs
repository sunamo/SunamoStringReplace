namespace SunamoStringReplace;

/// <summary>
/// Provides additional methods for replacing content within strings (partial class).
/// </summary>
public partial class SHReplace
{
    /// <summary>
    /// Replaces all occurrences of a search string except those prefixed with a specific string.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="forbiddenPrefix">The prefix that prevents replacement when found before the search string.</param>
    /// <returns>The text with non-prefixed occurrences replaced.</returns>
    public static string ReplaceAllExceptPrefixed(string text, string replacement, string what, string forbiddenPrefix)
    {
        var occurrences = SH.ReturnOccurencesOfString(text, what);
        for (var i = occurrences.Count - 1; i >= 0; i--)
        {
            var occurrence = occurrences[i];
            var prefixStart = occurrence - forbiddenPrefix.Length;
            if (prefixStart > -1)
            {
                var prefix = text.Substring(prefixStart, forbiddenPrefix.Length);
                if (prefix != forbiddenPrefix)
                    text = ReplaceByIndex(text, replacement, occurrence, what.Length);
            }
        }

        return text;
    }

    /// <summary>
    /// Replaces template variables enclosed in curly braces in HTML content.
    /// </summary>
    /// <param name="innerHtml">The HTML content containing variable references.</param>
    /// <param name="dataBinding">The data binding table with variable values.</param>
    /// <param name="actualRow">The row index to use for variable values.</param>
    /// <returns>The HTML content with variables replaced by their values.</returns>
    public static string ReplaceVariables(string innerHtml, List<List<string>> dataBinding, int actualRow)
    {
        return ReplaceVariables('{', '}', innerHtml, dataBinding, actualRow);
    }

    /// <summary>
    /// Replaces all occurrences using swapped argument order for convenience.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <returns>The text with replacements applied.</returns>
    public static string ReplaceAllDnArgs(string text, string what, string replacement)
    {
        return ReplaceAll(text, replacement, what);
    }

    /// <summary>
    /// Replaces a single search string with a replacement string in the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to search for.</param>
    /// <returns>The text with the replacement applied.</returns>
    public static string ReplaceAll2(string text, string replacement, string what)
    {
        return text.Replace(what, replacement);
    }

    /// <summary>
    /// Replaces all occurrences of multiple search strings with a single replacement.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="searchValues">The strings to search for and replace.</param>
    /// <returns>The text with all replacements applied.</returns>
    public static string ReplaceAll(string text, string replacement, params string[] searchValues)
    {
        return ReplaceAllArray(text, replacement, searchValues);
    }

    /// <summary>
    /// Replaces all occurrences of multiple search strings with a single replacement.
    /// If you want to replace multiline content with various indent use ReplaceAllDoubleSpaceToSingle2 to every variable
    /// which you are passed.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="searchValues">The strings to search for and replace.</param>
    /// <returns>The text with all replacements applied.</returns>
    public static string ReplaceAllArray(string text, string replacement, params string[] searchValues)
    {
        foreach (var element in searchValues)
            if (string.IsNullOrEmpty(element))
                return text;
        foreach (var element in searchValues)
            text = text.Replace(element, replacement);
        return text;
    }

    /// <summary>
    /// Replaces a search string with a replacement string, with optional validation.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="isAllowingSameWhatAndReplacement">Whether to allow search and replacement strings to be the same.</param>
    /// <param name="isThrowingExIfNotContains">Whether to throw an exception if the search string is not found.</param>
    /// <returns>The text with the replacement applied.</returns>
    public static string Replace(string text, string what, string replacement, bool isAllowingSameWhatAndReplacement = false, bool isThrowingExIfNotContains = false)
    {
        if (string.IsNullOrEmpty(replacement))
        {
            throw new ArgumentException($"{nameof(replacement)} is null or empty!");
        }

        if (!text.Contains(what) && isThrowingExIfNotContains)
        {
            throw new Exception($"{text} not contains {what}");
        }

        if (what == replacement)
        {
            if (isAllowingSameWhatAndReplacement)
                return text;
            ThrowEx.IsTheSame("what", "replacement");
        }

        var result = text.Replace(what, replacement);
        return result;
    }

    /// <summary>
    /// Replaces the last occurrence of a search string in the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <returns>The text with the last occurrence replaced.</returns>
    public static string ReplaceLastOccurenceOfString(string text, string what, string replacement)
    {
        var parts = SHSplit.Split(text, what);
        if (parts.Count == 1)
            return text.Replace(what, replacement);
        var resultBuilder = new StringBuilder();
        for (var i = 0; i < parts.Count - 2; i++)
            resultBuilder.Append(parts[i] + what);
        resultBuilder.Append(parts[parts.Count - 2]);
        resultBuilder.Append(replacement);
        resultBuilder.Append(parts[parts.Count - 1]);
        return resultBuilder.ToString();
    }

    /// <summary>
    /// Replaces the first occurrence of a search string, only if it appears before a specified character.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="maxToFirstChar">The character that limits the search range.</param>
    /// <returns>The text with the first occurrence replaced if within range.</returns>
    public static string ReplaceFirstOccurences(string text, string replacement, string what, char maxToFirstChar)
    {
        var whatIndex = text.IndexOf(what);
        if (whatIndex == -1)
            return text;
        var charIndex = text.IndexOf(maxToFirstChar);
        if (charIndex == -1)
            charIndex = text.Length;
        if (whatIndex > charIndex)
            return text;
        return ReplaceOnce(text, what, replacement);
    }

    /// <summary>
    /// Replaces the first occurrence of a search string in the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The string to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <returns>The text with the first occurrence replaced.</returns>
    public static string ReplaceFirstOccurences(string text, string what, string replacement)
    {
        var firstIndex = text.IndexOf(what);
        if (firstIndex != -1)
        {
            text = ReplaceOnce(text, what, replacement);
            text = text.Insert(firstIndex, replacement);
        }

        return text;
    }

    /// <summary>
    /// Replaces the second and subsequent occurrences of a pattern in the text.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="what">The regex pattern to search for.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <returns>The text with second and subsequent occurrences replaced.</returns>
    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string text, string what, string replacement)
    {
        var regex = new Regex(what);
        var firstIndex = text.IndexOf(what);
        if (firstIndex != -1)
            return regex.Replace(text, replacement, int.MaxValue, firstIndex + what.Length);
        return text;
    }

    /// <summary>
    /// Replaces all double spaces by splitting on whitespace and rejoining with single spaces.
    /// Works better than ReplaceAllDoubleSpaceToSingle when copying from webpages.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="isAlsoReplacingHtml">Whether to also replace HTML non-breaking spaces.</param>
    /// <returns>The text with all multiple spaces normalized to single spaces.</returns>
    public static string ReplaceAllDoubleSpaceToSingle2(string text, bool isAlsoReplacingHtml = false)
    {
        if (isAlsoReplacingHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        WhitespaceCharService whitespaceCharService = new WhitespaceCharService();
        var parts = SHSplit.Split(text, whitespaceCharService.WhiteSpaceChars.ConvertAll(character => character.ToString()).ToArray());
        return string.Join(" ", parts);
    }

    /// <summary>
    /// Removes whitespace characters (carriage return, newline, tab) from the text, excluding spaces.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <returns>The text with non-space whitespace characters removed.</returns>
    public static string ReplaceWhiteSpacesExcludeSpaces(string text)
    {
        return text.Replace("\r", "").Replace("\n", "").Replace("\t", "");
    }

    /// <summary>
    /// Replaces all occurrences of search strings in a case-insensitive manner.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace with.</param>
    /// <param name="searchValues">The strings to search for and replace.</param>
    /// <returns>The text with case-insensitive replacements applied.</returns>
    public static string ReplaceAllCaseInsensitive(string text, string replacement, params string[] searchValues)
    {
        foreach (var element in searchValues)
            if (replacement.Contains(element))
                throw new Exception("Replaced element " + element + " is part of replacement string " + replacement + ".");
        for (var i = 0; i < searchValues.Length; i++)
            text = Regex.Replace(text, searchValues[i], replacement, RegexOptions.IgnoreCase);
        return text;
    }

    /// <summary>
    /// Replaces every whitespace character with an empty string.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <returns>The text with all whitespace characters removed.</returns>
    public static string ReplaceWhiteSpaces(string text)
    {
        return ReplaceWhiteSpaces(text, "");
    }

    /// <summary>
    /// Replaces all whitespace characters except spaces first, then replaces the result with the specified replacement.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="replacement">The string to replace whitespace with.</param>
    /// <returns>The text with whitespace characters replaced.</returns>
    public static string ReplaceWhiteSpaces(string text, string replacement)
    {
        var replaced = ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(text, replacement);
        return Replace(replaced, "", replacement, true);
    }

    /// <summary>
    /// Replaces content in the text using a multiline mapping string with a custom delimiter.
    /// </summary>
    /// <param name="text">The text to process.</param>
    /// <param name="mappingText">The mapping text with lines containing from/to pairs separated by the delimiter.</param>
    /// <param name="delimiter">The delimiter separating from and to values in each line.</param>
    /// <returns>The text with all mapped replacements applied.</returns>
    public static string ReplaceManyFromString(string text, string mappingText, string delimiter)
    {
        var lines = SHGetLines.GetLines(mappingText);
        foreach (var line in lines)
        {
            var parts = SHSplit.Split(line, delimiter);
            parts = parts.ConvertAll(element => element.Trim());
            string fromValue, toValue;
            fromValue = toValue = null!;
            if (parts.Count > 0)
                fromValue = parts[0];
            else
                throw new Exception(line + " hasn't from");
            if (parts.Count > 1)
                toValue = parts[1];
            else
                throw new Exception(line + " hasn't to");
            if (WildcardHelper.IsWildcard(line))
            {
                var wildcard = new Wildcard(fromValue);
                ThrowEx.NotImplementedMethod();
            }
            else
            {
                text = ReplaceAll(text, toValue, fromValue);
            }
        }

        return text;
    }
}
