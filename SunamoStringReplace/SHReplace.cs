namespace SunamoStringReplace;

public partial class SHReplace
{
    private static readonly StringBuilder stringBuilder = new();

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

    public static string ReplaceAllDoubleSpaceToSingle(string text)
    {
        return ReplaceAllDoubleSpaceToSingle(text, false);
    }

    public static string ReplaceRef(ref string text, string what, string replacement)
    {
        text = text.Replace(what, replacement);
        return text;
    }

    public static string ReplaceFromEnd(string text, string replacement, string what)
    {
        var occurrences = SH.ReturnOccurencesOfString(text, what);
        for (var i = occurrences.Count - 1; i >= 0; i--)
            text = ReplaceByIndex(text, replacement, occurrences[i], what.Length);
        return text;
    }

    public static string ReplaceWhitespaces(string text, string replacement)
    {
        WhitespaceCharService whitespaceCharService = new();
        foreach (var character in whitespaceCharService.WhiteSpaceChars)
            text = text.Replace(character.ToString(), replacement);
        return text;
    }

    public static string ReplaceWhiteSpacesAndTrim(string text)
    {
        return ReplaceWhiteSpaces(text).Trim();
    }

    public static string ReplaceWhiteSpacesWithoutSpaces(string text, string replacement)
    {
        return ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(text, replacement);
    }

    public static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string text, string replacement)
    {
        return text.Replace("\r", replacement).Replace("\n", replacement).Replace("\t", replacement);
    }

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

    public static string ReplaceByIndex(string text, string replacement, int index, int length)
    {
        text = text.Remove(index, length);
        if (replacement != string.Empty)
            text = text.Insert(index, replacement);
        return text;
    }

    public static StringBuilder ReplaceByIndex(StringBuilder builder, string replacement, int index, int length)
    {
        builder = builder.Remove(index, length);
        if (replacement != string.Empty)
            builder = builder.Insert(index, replacement);
        return builder;
    }

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

    public static string ReplaceAllWhitecharsForSpace(string text)
    {
        WhitespaceCharService whitespaceCharService = new();
        foreach (var character in whitespaceCharService.WhiteSpaceChars)
            if (character != ' ')
                text = text.Replace(character, ' ');
        return text;
    }
}
