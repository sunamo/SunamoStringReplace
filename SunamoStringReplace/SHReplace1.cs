namespace SunamoStringReplace;

public partial class SHReplace
{
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

    public static string ReplaceVariables(string innerHtml, List<List<string>> dataBinding, int actualRow)
    {
        return ReplaceVariables('{', '}', innerHtml, dataBinding, actualRow);
    }

    public static string ReplaceAllDnArgs(string text, string what, string replacement)
    {
        return ReplaceAll(text, replacement, what);
    }

    public static string ReplaceAll2(string text, string replacement, string what)
    {
        return text.Replace(what, replacement);
    }

    public static string ReplaceAll(string text, string replacement, params string[] searchValues)
    {
        return ReplaceAllArray(text, replacement, searchValues);
    }

    // If you want to replace multiline content with various indent use ReplaceAllDoubleSpaceToSingle2 to every variable
    // which you are passed.
    public static string ReplaceAllArray(string text, string replacement, params string[] searchValues)
    {
        foreach (var element in searchValues)
            if (string.IsNullOrEmpty(element))
                return text;
        foreach (var element in searchValues)
            text = text.Replace(element, replacement);
        return text;
    }

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

    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string text, string what, string replacement)
    {
        var regex = new Regex(what);
        var firstIndex = text.IndexOf(what);
        if (firstIndex != -1)
            return regex.Replace(text, replacement, int.MaxValue, firstIndex + what.Length);
        return text;
    }

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

    public static string ReplaceWhiteSpacesExcludeSpaces(string text)
    {
        return text.Replace("\r", "").Replace("\n", "").Replace("\t", "");
    }

    public static string ReplaceAllCaseInsensitive(string text, string replacement, params string[] searchValues)
    {
        foreach (var element in searchValues)
            if (replacement.Contains(element))
                throw new Exception("Replaced element " + element + " is part of replacement string " + replacement + ".");
        for (var i = 0; i < searchValues.Length; i++)
            text = Regex.Replace(text, searchValues[i], replacement, RegexOptions.IgnoreCase);
        return text;
    }

    public static string ReplaceWhiteSpaces(string text)
    {
        return ReplaceWhiteSpaces(text, "");
    }

    public static string ReplaceWhiteSpaces(string text, string replacement)
    {
        var replaced = ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(text, replacement);
        return Replace(replaced, "", replacement, true);
    }

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
