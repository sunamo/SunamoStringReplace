namespace SunamoStringReplace;

public partial class SHReplace
{
    /// <summary>
    /// Replace all except prefixed.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="what">The what.</param>
    /// <param name="forbiddenPrefix">The forbidden prefix.</param>
    /// <returns>The replace all except prefixed result.</returns>
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
    /// Replace variables.
    /// </summary>
    /// <param name="innerHtml">The inner html.</param>
    /// <param name="dataBinding">The data binding.</param>
    /// <param name="actualRow">The actual row.</param>
    /// <returns>The replace variables result.</returns>
    public static string ReplaceVariables(string innerHtml, List<List<string>> dataBinding, int actualRow)
    {
        return ReplaceVariables('{', '}', innerHtml, dataBinding, actualRow);
    }

    /// <summary>
    /// Replace all dn args.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="what">The what.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace all dn args result.</returns>
    public static string ReplaceAllDnArgs(string text, string what, string replacement)
    {
        return ReplaceAll(text, replacement, what);
    }

    /// <summary>
    /// Replace all 2.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="what">The what.</param>
    /// <returns>The replace all 2 result.</returns>
    public static string ReplaceAll2(string text, string replacement, string what)
    {
        return text.Replace(what, replacement);
    }

    /// <summary>
    /// Replace all.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="searchValues">The search values.</param>
    /// <returns>The replace all result.</returns>
    public static string ReplaceAll(string text, string replacement, params string[] searchValues)
    {
        return ReplaceAllArray(text, replacement, searchValues);
    }

    // If you want to replace multiline content with various indent use ReplaceAllDoubleSpaceToSingle2 to every variable
    // which you are passed.
    /// <summary>
    /// Replace all array.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="searchValues">The search values.</param>
    /// <returns>The replace all array result.</returns>
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
    /// Replace.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="what">The what.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="isAllowingSameWhatAndReplacement">The is allowing same what and replacement.</param>
    /// <param name="isThrowingExIfNotContains">The is throwing ex if not contains.</param>
    /// <returns>The replace result.</returns>
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
    /// Replace last occurence of string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="what">The what.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace last occurence of string result.</returns>
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
    /// Replace first occurences.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="what">The what.</param>
    /// <param name="maxToFirstChar">The max to first char.</param>
    /// <returns>The replace first occurences result.</returns>
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
    /// Replace first occurences.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="what">The what.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace first occurences result.</returns>
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
    /// Replace second and next occurences of string from.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="what">The what.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace second and next occurences of string from result.</returns>
    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string text, string what, string replacement)
    {
        var regex = new Regex(what);
        var firstIndex = text.IndexOf(what);
        if (firstIndex != -1)
            return regex.Replace(text, replacement, int.MaxValue, firstIndex + what.Length);
        return text;
    }

    /// <summary>
    /// Replace all double space to single 2.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="isAlsoReplacingHtml">The is also replacing html.</param>
    /// <returns>The replace all double space to single 2 result.</returns>
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
    /// Replace white spaces exclude spaces.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The replace white spaces exclude spaces result.</returns>
    public static string ReplaceWhiteSpacesExcludeSpaces(string text)
    {
        return text.Replace("\r", "").Replace("\n", "").Replace("\t", "");
    }

    /// <summary>
    /// Replace all case insensitive.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="searchValues">The search values.</param>
    /// <returns>The replace all case insensitive result.</returns>
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
    /// Replace white spaces.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The replace white spaces result.</returns>
    public static string ReplaceWhiteSpaces(string text)
    {
        return ReplaceWhiteSpaces(text, "");
    }

    /// <summary>
    /// Replace white spaces.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace white spaces result.</returns>
    public static string ReplaceWhiteSpaces(string text, string replacement)
    {
        var replaced = ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(text, replacement);
        return Replace(replaced, "", replacement, true);
    }

    /// <summary>
    /// Replace many from string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="mappingText">The mapping text.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The replace many from string result.</returns>
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
