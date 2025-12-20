// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringReplace;
public partial class SHReplace
{
    private static readonly StringBuilder stringBuilder = new();
    /// <summary>
    ///     Working - see unit tests
    /// </summary>
    /// <param name = "text"></param>
    /// <param name = "alsoHtml"></param>
    /// <returns></returns>
    public static string ReplaceAllDoubleSpaceToSingle(string text, bool alsoHtml = false)
    {
        //text = SH.FromSpace160To32(text);
        if (alsoHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        while (text.Contains("  "))
            text = ReplaceAll2(text, " ", "  ");
        // Here it was cycling, dont know why, therefore without while
        //while (text.Contains("space160 + space"))
        //{
        //text = ReplaceAll2(text, "", "space160 + space");
        //}
        //while (text.Contains("space + space160"))
        //{
        //text = ReplaceAll2(text, "", "space + space160");
        //}
        return text;
    }

    public static List<string> SplitAdvanced(string value, bool replaceNewLineBySpace, bool moreSpacesForOne, bool _trim, bool escapeQuoations, params string[] deli)
    {
        var text = value.Split(deli, StringSplitOptions.None).ToList();
        if (replaceNewLineBySpace)
            for (var i = 0; i < text.Count; i++)
                text[i] = ReplaceAll(text[i], "", "\r", @"\n", Environment.NewLine);
        if (moreSpacesForOne)
            for (var i = 0; i < text.Count; i++)
                text[i] = ReplaceAll2(text[i], " ", "");
        if (_trim)
            text = text.ConvertAll(d => d.Trim());
        if (escapeQuoations)
        {
            var rep = "\"";
            for (var i = 0; i < text.Count; i++)
                text[i] = ReplaceFromEnd(text[i], "\"", rep);
        //}
        }

        return text;
    }

    /// <summary>
    ///     Working - see unit tests
    /// </summary>
    /// <param name = "text"></param>
    /// <returns></returns>
    public static string ReplaceAllDoubleSpaceToSingle(string text)
    {
        return ReplaceAllDoubleSpaceToSingle(text, false);
    }

    public static string ReplaceRef(ref string resultStatus, string what, string forWhat)
    {
        resultStatus = resultStatus.Replace(what, forWhat);
        return resultStatus;
    }

    public static string ReplaceFromEnd(string text, string zaCo, string co)
    {
        var occ = SH.ReturnOccurencesOfString(text, co);
        for (var i = occ.Count - 1; i >= 0; i--)
            text = ReplaceByIndex(text, zaCo, occ[i], co.Length);
        return text;
    }

    /// <summary>
    ///     Replace AllChars.whiteSpaceChars with A2
    /// </summary>
    /// <param name = "s"></param>
    /// <param name = "forWhat"></param>
    /// <returns></returns>
    public static string ReplaceWhitespaces(string text, string forWhat)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        foreach (var value in whitespaceChar.whiteSpaceChars)
            text = text.Replace(value.ToString(), forWhat);
        return text;
    }

    public static string ReplaceWhiteSpacesAndTrim(string parameter)
    {
        return ReplaceWhiteSpaces(parameter).Trim();
    }

    public static string ReplaceWhiteSpacesWithoutSpaces(string parameter, string replaceWith)
    {
        return ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(parameter, replaceWith);
    }

    /// <summary>
    ///     Replace result,n,t with A2
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "replaceWith"></param>
    /// <returns></returns>
    public static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string parameter, string replaceWith)
    {
        return parameter.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    public static string ReplaceVariables(char parameter, char k, string innerHtml, List<List<string>> _dataBinding, int actualRow)
    {
        var sbNepridano = new StringBuilder();
        var sbPridano = new StringBuilder();
        var inVariable = false;
        if (innerHtml != null)
            foreach (var item in innerHtml)
                if (item == parameter)
                {
                    inVariable = true;
                }
                else if (item == k)
                {
                    if (inVariable)
                        inVariable = false;
                    var nt = 0;
                    if (int.TryParse(sbNepridano.ToString(), out nt))
                    {
                        // Zde přidat nahrazenou proměnnou
                        var value = _dataBinding[nt][actualRow];
                        sbPridano.Append(value);
                    }
                    else
                    {
                        sbPridano.Append(parameter + sbNepridano.ToString() + k);
                    }

                    sbNepridano.Clear();
                }
                else if (inVariable)
                {
                    sbNepridano.Append(item);
                }
                else
                {
                    sbPridano.Append(item);
                }

        return sbPridano.ToString();
    }

    public static string ReplaceByIndex(string text, string zaCo, int value, int length)
    {
        text = text.Remove(value, length);
        if (zaCo != string.Empty)
            text = text.Insert(value, zaCo);
        return text;
    }

    public static StringBuilder ReplaceByIndex(StringBuilder text, string zaCo, int value, int length)
    {
        text = text.Remove(value, length);
        if (zaCo != string.Empty)
            text = text.Insert(value, zaCo);
        return text;
    }

    /// <summary>
    ///     Overload is without bool pairLines
    /// </summary>
    /// <param name = "vstup"></param>
    /// <param name = "zaCo"></param>
    /// <param name = "co"></param>
    /// <param name = "pairLines"></param>
    public static string ReplaceAll2(string vstup, string zaCo, string co, bool pairLines)
    {
        if (pairLines)
        {
            var from2 = SHSplit.Split(co, Environment.NewLine);
            var to2 = SHSplit.Split(zaCo, Environment.NewLine);
            ThrowEx.DifferentCountInLists("from2", from2, "to2", to2);
            for (var i = 0; i < from2.Count; i++)
                vstup = ReplaceAll2(vstup, to2[i], from2[i]);
            return vstup;
        }

        return ReplaceAll2(vstup, zaCo, co);
    }

    public static StringBuilder ReplaceAllSb(StringBuilder stringBuilder, string zaCo, params string[] co)
    {
        foreach (var oldValue in co)
        {
            if (oldValue == zaCo)
                continue;
            stringBuilder = stringBuilder.Replace(oldValue, zaCo);
        }

        return stringBuilder;
    }

    public static string ReplaceMany(string input, string fromTo, bool removeEndingPairCharsWhenDontHaveStarting = true)
    {
        var from = new StringBuilder();
        var to = new StringBuilder();
        var list = SHGetLines.GetLines(fromTo);
        list = list.Where(d => d.Trim() != string.Empty).ToList();
        var delimiter = "->";
        var replaceForEmpty = new List<string>();
        foreach (var mappingLine in list)
        {
            // Must be split, not splitNone
            // 'ReplaceInAllFiles:  Different count elements in collection from2 - 4 vs. to2 - 3'
            var parameter = SHSplit.Split(mappingLine, delimiter);
            if (parameter.Count == 1)
                if (mappingLine.EndsWith(delimiter))
                {
                    replaceForEmpty.Add(parameter[0]);
                    continue;
                //parameter.Add(string.Empty);
                }

            //parameter.Insert(0, string.Empty);
            from.AppendLine(parameter[0]);
            to.AppendLine(parameter[1]);
        }

        var vr = ReplaceAll2(input, to.ToString(), from.ToString(), true);
        foreach (var oldValue in replaceForEmpty)
            vr = vr.Replace(oldValue, string.Empty);
        if (removeEndingPairCharsWhenDontHaveStarting)
            vr = SH.RemoveEndingPairCharsWhenDontHaveStarting(vr, "{", "}");
        return vr;
    }

    /// <summary>
    ///     Method is useless
    ///     ReplaceMany firstly split into two strings
    ///     Better is call ReplaceAll2(input, to.ToString(), from.ToString(), true);
    /// </summary>
    /// <param name = "from"></param>
    /// <param name = "to"></param>
    /// <returns></returns>
     //public static string PrepareForReplaceMany(List<string> from, List<string> to)
    //{
    //    return null;
    //}
    public static string ReplaceAllWhitecharsForSpace(string count)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        foreach (var value in whitespaceChar.whiteSpaceChars)
            if (value != ' ')
                count = count.Replace(value, ' ');
        return count;
    }
}