// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoStringReplace;

public class SHReplace //: SHData
{
    private static readonly StringBuilder stringBuilder = new();

    /// <summary>
    ///     Working - see unit tests
    /// </summary>
    /// <param name="text"></param>
    /// <param name="alsoHtml"></param>
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

    public static List<string> SplitAdvanced(string value, bool replaceNewLineBySpace, bool moreSpacesForOne, bool _trim,
        bool escapeQuoations, params string[] deli)
    {
        var text = value.Split(deli, StringSplitOptions.None).ToList();
        if (replaceNewLineBySpace)
            for (var i = 0; i < text.Count; i++)
                text[i] = ReplaceAll(text[i], "", "\r", @"\n", Environment.NewLine);
        if (moreSpacesForOne)
            for (var i = 0; i < text.Count; i++)
                text[i] = ReplaceAll2(text[i], " ", "");
        if (_trim) text = text.ConvertAll(d => d.Trim());
        if (escapeQuoations)
        {
            var rep = "\"";
            for (var i = 0; i < text.Count; i++) text[i] = ReplaceFromEnd(text[i], "\"", rep);
            //}
        }

        return text;
    }

    /// <summary>
    ///     Working - see unit tests
    /// </summary>
    /// <param name="text"></param>
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
        for (var i = occ.Count - 1; i >= 0; i--) text = ReplaceByIndex(text, zaCo, occ[i], co.Length);
        return text;
    }

    /// <summary>
    ///     Replace AllChars.whiteSpaceChars with A2
    /// </summary>
    /// <param name="s"></param>
    /// <param name="forWhat"></param>
    /// <returns></returns>
    public static string ReplaceWhitespaces(string text, string forWhat)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        foreach (var value in whitespaceChar.whiteSpaceChars) text = text.Replace(value.ToString(), forWhat);
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
    /// <param name="p"></param>
    /// <param name="replaceWith"></param>
    /// <returns></returns>
    public static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string parameter, string replaceWith)
    {
        return parameter.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    public static string ReplaceVariables(char parameter, char k, string innerHtml, List<List<string>> _dataBinding,
        int actualRow)
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
                    if (inVariable) inVariable = false;
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
        if (zaCo != string.Empty) text = text.Insert(value, zaCo);
        return text;
    }

    public static StringBuilder ReplaceByIndex(StringBuilder text, string zaCo, int value, int length)
    {
        text = text.Remove(value, length);
        if (zaCo != string.Empty) text = text.Insert(value, zaCo);
        return text;
    }

    /// <summary>
    ///     Overload is without bool pairLines
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    /// <param name="pairLines"></param>
    public static string ReplaceAll2(string vstup, string zaCo, string co, bool pairLines)
    {
        if (pairLines)
        {
            var from2 = SHSplit.Split(co, Environment.NewLine);
            var to2 = SHSplit.Split(zaCo, Environment.NewLine);
            ThrowEx.DifferentCountInLists("from2", from2, "to2", to2);
            for (var i = 0; i < from2.Count; i++) vstup = ReplaceAll2(vstup, to2[i], from2[i]);
            return vstup;
        }

        return ReplaceAll2(vstup, zaCo, co);
    }

    public static StringBuilder ReplaceAllSb(StringBuilder stringBuilder, string zaCo, params string[] co)
    {
        foreach (var oldValue in co)
        {
            if (oldValue == zaCo) continue;
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
        foreach (var oldValue in replaceForEmpty) vr = vr.Replace(oldValue, string.Empty);
        if (removeEndingPairCharsWhenDontHaveStarting)
            vr = SH.RemoveEndingPairCharsWhenDontHaveStarting(vr, "{", "}");
        return vr;
    }

    /// <summary>
    ///     Method is useless
    ///     ReplaceMany firstly split into two strings
    ///     Better is call ReplaceAll2(input, to.ToString(), from.ToString(), true);
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
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

    public static string ReplaceAllExceptPrefixed(string t, string to, string from, string fromCannotBePrefixed)
    {
        var occ = SH.ReturnOccurencesOfString(t, from);
        for (var i = occ.Count - 1; i >= 0; i--)
        {
            var item = occ[i];
            var begin = item - fromCannotBePrefixed.Length;
            if (begin > -1)
            {
                var prefix = t.Substring(begin, fromCannotBePrefixed.Length);
                if (prefix != fromCannotBePrefixed) t = ReplaceByIndex(t, to, item, from.Length);
            }
        }

        return t;
    }

    public static string ReplaceVariables(string innerHtml, List<List<string>> _dataBinding, int actualRow)
    {
        return ReplaceVariables('{', '}', innerHtml, _dataBinding, actualRow);
    }

    public static string ReplaceAllDnArgs(string input, string v1, string v2)
    {
        return ReplaceAll(input, v2, v1);
    }

    /// <summary>
    ///     Stejná jako metoda ReplaceAll, ale bere si do A3 pouze jediný parametr, nikoliv jejich pole
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    public static string ReplaceAll2(string vstup, string zaCo, string co)
    {
        return vstup.Replace(co, zaCo);
    }

    /// <summary>
    ///     Protože jsem zapomínal na to že jsem ReplaceAll nahradil za ReplaceAllArray dávám tu zpět i tento starý název,
    ///     dokud zase nebude někomu vadit
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    /// <returns></returns>
    public static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {
        return ReplaceAllArray(vstup, zaCo, co);
    }

    /// <summary>
    ///     If you want to replace multiline content with various indent use ReplaceAllDoubleSpaceToSingle2 to every variable
    ///     which you are passed
    /// </summary>
    /// <param name="vstup"></param>
    /// <param name="zaCo"></param>
    /// <param name="co"></param>
    public static string ReplaceAllArray(string vstup, string zaCo, params string[] co)
    {
        foreach (var item in co)
            if (string.IsNullOrEmpty(item))
                return vstup;
        foreach (var item in co) vstup = vstup.Replace(item, zaCo);
        return vstup;
    }

    /// <summary>
    ///     Use simple count# replace
    ///     18-5-2023
    ///     Nevím zda se tato metoda měnila. Ale měl jsem u jejího volání přehozené A2,3
    ///     Zatím to nechám jak je.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static string Replace(string t, string what, string forWhat, bool a2CanBeAsA3 = false, bool throwExIfNotContains = false)
    {
        if (string.IsNullOrEmpty(forWhat))
        {
            throw new ArgumentException($"{what} is null or empty!");
        }

        if (!t.Contains(what) && throwExIfNotContains)
        {
            throw new Exception($"{t} not contains {what}");
        }

        if (what == forWhat)
        {
            if (a2CanBeAsA3)
                return t;
            ThrowEx.IsTheSame("what", "forWhat");
        }

        var result = t.Replace(what, forWhat);
        return result;
    }

    public static string ReplaceLastOccurenceOfString(string text, string co, string čím)
    {
        var roz = SHSplit.Split(text, co);
        if (roz.Count == 1) return text.Replace(co, čím);

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < roz.Count - 2; i++) stringBuilder.Append(roz[i] + co);
        stringBuilder.Append(roz[roz.Count - 2]);
        stringBuilder.Append(čím);
        stringBuilder.Append(roz[roz.Count - 1]);
        return stringBuilder.ToString();
    }

    public static string ReplaceFirstOccurences(string value, string zaCo, string co, char maxToFirstChar)
    {
        var dexCo = value.IndexOf(co);
        if (dexCo == -1) return value;
        var dex = value.IndexOf(maxToFirstChar);
        if (dex == -1) dex = value.Length;
        if (dexCo > dex) return value;
        return ReplaceOnce(value, co, zaCo);
    }

    public static string ReplaceFirstOccurences(string text, string co, string zaCo)
    {
        var fi = text.IndexOf(co);
        if (fi != -1)
        {
            text = ReplaceOnce(text, co, zaCo);
            text = text.Insert(fi, zaCo);
        }

        return text;
    }

    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string vcem2, string co, string zaCo/*,
        int overallCountOfA2*/)
    {
        var result = new Regex(co);
        //StringBuilder vcem = new StringBuilder(vcem2);
        var dex = vcem2.IndexOf(co);
        if (dex != -1) return result.Replace(vcem2, zaCo, int.MaxValue, dex + co.Length);
        //return vcem.Replace(co, zaCo, dex + co.Length , overallCountOfA2 - 1 ).ToString();
        return vcem2;
    }

    /// <summary>
    ///     Working - see unit tests
    ///     Split by all whitespaces - remove also newline
    ///     ReplaceAllDoubleSpaceToSingle not working correctly while copy from webpage
    ///     Split and join again
    /// </summary>
    /// <param name="text"></param>
    public static string ReplaceAllDoubleSpaceToSingle2(string text, bool alsoHtml = false)
    {
        if (alsoHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        WhitespaceCharService whitespaceChar = new WhitespaceCharService();

        var parameter = SHSplit.Split(text, whitespaceChar.whiteSpaceChars.ConvertAll(d => d.ToString()).ToArray());
        return string.Join(" ", parameter);
    }

    public static string ReplaceWhiteSpacesExcludeSpaces(string parameter)
    {
        return parameter.Replace("\r", "").Replace("\n", "").Replace("\t", "");
    }

    public static string ReplaceAllCaseInsensitive(string vr, string zaCo, params string[] co)
    {
        foreach (var item in co)
            if (zaCo.Contains(item))
                throw new Exception("Nahrazovan\u00FD prvek " + item + " je prvkem j\u00EDm\u017E se nahrazuje  " +
                                    zaCo + ".");
        for (var i = 0; i < co.Length; i++) vr = Regex.Replace(vr, co[i], zaCo, RegexOptions.IgnoreCase);
        return vr;
    }

    /// <summary>
    ///     Replace every whitespace with empty string
    /// </summary>
    /// <param name="replaceWith"></param>
    public static string ReplaceWhiteSpaces(string replaceWith)
    {
        return ReplaceWhiteSpaces(replaceWith, "");
    }

    /// <summary>
    ///     Replace all whitespaces except of space and then A1 with space
    ///     In other words, in finale, replace every whitespace with space - A2 is for better customizing
    ///     A2 can be also space
    /// </summary>
    /// <param name="p"></param>
    /// <param name="replaceWith"></param>
    public static string ReplaceWhiteSpaces(string parameter, string replaceWith)
    {
        var replaced = ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(parameter, replaceWith);
        return Replace(replaced, "", replaceWith, true);
    }

    /// <summary>
    ///     A2 a->b
    ///     A3 ->
    /// </summary>
    /// <param name="input"></param>
    /// <param name="v"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static string ReplaceManyFromString(string input, string value, string delimiter)
    {
        var methodName = "ReplaceManyFromString";
        var list = SHGetLines.GetLines(value);
        foreach (var item in list)
        {
            var parameter = SHSplit.Split(item, delimiter);
            parameter = parameter.ConvertAll(d => d.Trim());
            string from, to;
            from = to = null;
            if (parameter.Count > 0)
                from = parameter[0];
            else
                throw new Exception(item + " hasn't from");
            if (parameter.Count > 1)
                to = parameter[1];
            else
                throw new Exception(item + " hasn't to");
            if (WildcardHelper.IsWildcard(item))
            {
                var wc = new Wildcard(from);
                ThrowEx.NotImplementedMethod();
                //var occurences = wc.Matches(input);
                //foreach (Match match in occurences)
                //{
                //    var result = match.Result();
                //    var groups = match.Groups;
                //    var captues = match.Captures;
                //    var value = match.Value;
                //}
            }
            else
            {
                //Wildcard wildcard = new Wildcard();
                input = ReplaceAll(input, to, from);
            }
        }

        return input;
    }

    public static string ReplaceFirstLine(string result, string from, string to)
    {
        var list = SHGetLines.GetLines(result);
        if (list[0] == from)
            list[0] = to;
        else
            throw new Exception($"First line is not excepted '{from}', was '{list[0]}'");
        return string.Join("\n", list);
    }

    public static string ReplaceAll4(string t, string to, string from)
    {
        while (t.Contains(from)) t = t.Replace(from, to);
        return t;
    }

    public static string ReplaceAll3(IList<string> replaceFrom, IList<string> replaceTo,
        bool isMultilineWithVariousIndent, string content)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();

        if (isMultilineWithVariousIndent)
            for (var i = 0; i < replaceFrom.Count; i++)
            {
                /*
                Vše zaměnit na 1 mezeru
                porovnat zaměněné a originál - namapovat co je mezi nimi
                */
                var replaceFromDxWithoutEmptyElements =
                    replaceFrom[i].Split(whitespaceChar.whiteSpaceChars.ToArray()).ToList();
                var contentWithoutEmptyElements = content.Split(whitespaceChar.whiteSpaceChars.ToArray()).ToList();
                ////DebugLogger.Instance.WriteNumberedList("", contentOneSpace, true);
                // get indexes
                var equalRanges = CAG.EqualRanges(contentWithoutEmptyElements, replaceFromDxWithoutEmptyElements);
                if (equalRanges.Count == 0) return content;
                var startDx = (int)equalRanges.FirstOrDefault().FromL;
                var endDx = (int)equalRanges.Last().ToL;
                // všechny elementy z contentOneSpace namapované na content kde value něm začínají.
                // index z nt odkazuje na content
                // proto musím vzít první a poslední index z equalRanges a k poslednímu přičíst contentOneSpace[last].Length
                var nt = new List<int>(contentWithoutEmptyElements.Count());
                var startFrom = 0;
                foreach (var item2 in contentWithoutEmptyElements)
                {
                    var dx = content.IndexOf(item2, startFrom);
                    startFrom = dx + item2.Length;
                    nt.Add(dx);
                }

                var replaceWhat = new StringBuilder();
                // Now I must iterate and add also white chars between
                //foreach (var ft in equalRanges)
                //{
                //    // Musím vzít index z nt
                //}
                var add = contentWithoutEmptyElements[endDx].Length;
                startDx = nt[startDx];
                endDx = nt[endDx];
                endDx += add;
                var from2 = content.Substring(startDx, endDx - startDx);
                content = content.Replace(from2, replaceTo[i]);
            }
        else
            for (var i = 0; i < replaceFrom.Count; i++)
                content = content.Replace(replaceFrom[i], replaceTo[i]);

        //if (SH.ContainsAny(content, false, replaceFrom).Count > 0)
        //{
        //}
        return content;
    }

    public static string ReplaceWithIndex(string n, string value, string empty, ref int dx)
    {
        if (dx == -1)
        {
            dx = n.IndexOf(value);
            if (dx != -1)
            {
                n = n.Remove(dx, value.Length);
                n = n.Insert(dx, empty);
            }
        }

        return n;
    }

    public static string ReplaceTypedWhitespacesForNormal(string t, bool quote, bool t24, bool bs)
    {
        stringBuilder.Clear();
        t = t.Trim();
        // jen zde protože jestli něco dělám přes ts tak to dělám na rychlost a to už musí být tohohle zbavené
        t = t.TrimEnd('"', '\'');
        stringBuilder.Append(t);
        return ReplaceTypedWhitespacesForNormal(stringBuilder, quote, t24, bs).ToString();
    }

    public static StringBuilder ReplaceTypedWhitespacesForNormal(StringBuilder t, bool quote, bool t24, bool bs)
    {
        if (t24) t = t.Replace("\\\\t24", string.Empty);
        t = t.Replace("\\t", "\t");
        t = t.Replace("\\n", "\n");
        t = t.Replace("\\r", "\r");
        if (quote) t = t.Replace("\\\"", "\"");
        if (bs) t = t.Replace("\\\\", "\\");
        //t = t.Replace("\\r", "\r");
        return t;
    }

    public static string ReplaceInLine(string list, string what, string to, bool checkForMoreOccurences)
    {
        var count = new List<string>(new[] { list }); //CAG.ToList(list);
        ReplaceInLine(count, 1, what, to, checkForMoreOccurences);
        return count[0];
    }

    public static void ReplaceInLine(List<string> list, int lineFromOne, string what, string to,
        bool checkForMoreOccurences)
    {
        if (checkForMoreOccurences)
        {
            var occ = SH.ReturnOccurencesOfString(list[lineFromOne - 1], what);
            if (occ.Count > 1)
                foreach (var item in occ)
                {
                    var after = list[lineFromOne - 1][item + what.Length];
                    if (after == ',' || after == ' ')
                    {
                        var text = list[lineFromOne - 1];
                        text = text.Remove(item, what.Length);
                        text = text.Insert(item, to);
                        list[lineFromOne - 1] = text;
                        break;
                    }
                }
            else
                list[lineFromOne - 1] = ReplaceOnce(list[lineFromOne - 1], what, to);
        }
        else
        {
            list[lineFromOne - 1] = ReplaceOnce(list[lineFromOne - 1], what, to);
        }
    }

    public static string ReplaceOnce(string input, string what, string zaco)
    {
        if (what == "") return input;
        var pos = input.IndexOf(what);
        if (pos == -1) return input;
        return input.Substring(0, pos) + zaco + input.Substring(pos + what.Length);
    }
}