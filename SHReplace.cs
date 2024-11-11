namespace SunamoStringReplace;

public class SHReplace //: SHData
{
    private static readonly StringBuilder sb = new();

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

    public static List<string> SplitAdvanced(string v, bool replaceNewLineBySpace, bool moreSpacesForOne, bool _trim,
        bool escapeQuoations, params string[] deli)
    {
        var s = v.Split(deli, StringSplitOptions.None).ToList();
        if (replaceNewLineBySpace)
            for (var i = 0; i < s.Count; i++)
                s[i] = ReplaceAll(s[i], "", "\r", @"\n", Environment.NewLine);
        if (moreSpacesForOne)
            for (var i = 0; i < s.Count; i++)
                s[i] = ReplaceAll2(s[i], " ", "");
        if (_trim) s = s.ConvertAll(d => d.Trim());
        if (escapeQuoations)
        {
            var rep = "\"";
            for (var i = 0; i < s.Count; i++) s[i] = ReplaceFromEnd(s[i], "\"", rep);
            //}
        }

        return s;
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

    public static string ReplaceFromEnd(string s, string zaCo, string co)
    {
        var occ = SH.ReturnOccurencesOfString(s, co);
        for (var i = occ.Count - 1; i >= 0; i--) s = ReplaceByIndex(s, zaCo, occ[i], co.Length);
        return s;
    }

    /// <summary>
    ///     Replace AllChars.whiteSpaceChars with A2
    /// </summary>
    /// <param name="s"></param>
    /// <param name="forWhat"></param>
    /// <returns></returns>
    public static string ReplaceWhitespaces(string s, string forWhat)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        foreach (var item in whitespaceChar.whiteSpaceChars) s = s.Replace(item.ToString(), forWhat);
        return s;
    }

    public static string ReplaceWhiteSpacesAndTrim(string p)
    {
        return ReplaceWhiteSpaces(p).Trim();
    }

    public static string ReplaceWhiteSpacesWithoutSpaces(string p, string replaceWith)
    {
        return ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(p, replaceWith);
    }

    /// <summary>
    ///     Replace r,n,t with A2
    /// </summary>
    /// <param name="p"></param>
    /// <param name="replaceWith"></param>
    /// <returns></returns>
    public static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string p, string replaceWith)
    {
        return p.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    public static string ReplaceVariables(char p, char k, string innerHtml, List<List<string>> _dataBinding,
        int actualRow)
    {
        var sbNepridano = new StringBuilder();
        var sbPridano = new StringBuilder();
        var inVariable = false;
        if (innerHtml != null)
            foreach (var item in innerHtml)
                if (item == p)
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
                        var v = _dataBinding[nt][actualRow];
                        sbPridano.Append(v);
                    }
                    else
                    {
                        sbPridano.Append(p + sbNepridano.ToString() + k);
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

    public static string ReplaceByIndex(string s, string zaCo, int v, int length)
    {
        s = s.Remove(v, length);
        if (zaCo != string.Empty) s = s.Insert(v, zaCo);
        return s;
    }

    public static StringBuilder ReplaceByIndex(StringBuilder s, string zaCo, int v, int length)
    {
        s = s.Remove(v, length);
        if (zaCo != string.Empty) s = s.Insert(v, zaCo);
        return s;
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
            var from2 = SHSplit.SplitMore(co, Environment.NewLine);
            var to2 = SHSplit.SplitMore(zaCo, Environment.NewLine);
            ThrowEx.DifferentCountInLists("from2", from2, "to2", to2);
            for (var i = 0; i < from2.Count; i++) vstup = ReplaceAll2(vstup, to2[i], from2[i]);
            return vstup;
        }

        return ReplaceAll2(vstup, zaCo, co);
    }

    public static StringBuilder ReplaceAllSb(StringBuilder sb, string zaCo, params string[] co)
    {
        foreach (var item in co)
        {
            if (item == zaCo) continue;
            sb = sb.Replace(item, zaCo);
        }

        return sb;
    }

    public static string ReplaceMany(string input, string fromTo, bool removeEndingPairCharsWhenDontHaveStarting = true)
    {
        var from = new StringBuilder();
        var to = new StringBuilder();
        var l = SHGetLines.GetLines(fromTo);
        l = l.Where(d => d.Trim() != string.Empty).ToList();
        var delimiter = "->";
        var replaceForEmpty = new List<string>();
        foreach (var item in l)
        {
            // Must be split, not splitNone
            // 'ReplaceInAllFiles:  Different count elements in collection from2 - 4 vs. to2 - 3'
            var p = SHSplit.SplitMore(item, delimiter);
            if (p.Count == 1)
                if (item.EndsWith(delimiter))
                {
                    replaceForEmpty.Add(p[0]);
                    continue;
                    //p.Add(string.Empty);
                }

            //p.Insert(0, string.Empty);
            from.AppendLine(p[0]);
            to.AppendLine(p[1]);
        }

        var vr = ReplaceAll2(input, to.ToString(), from.ToString(), true);
        foreach (var item in replaceForEmpty) vr = vr.Replace(item, string.Empty);
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
    public static string PrepareForReplaceMany(List<string> from, List<string> to)
    {
        return null;
    }

    public static string ReplaceAllWhitecharsForSpace(string c)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        foreach (var item in whitespaceChar.whiteSpaceChars)
            if (item != ' ')
                c = c.Replace(item, ' ');
        return c;
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

    public static string Replace(string t, string what, string forWhat)
    {
        return Replace(t, what, forWhat, false);
    }

    /// <summary>
    ///     Use simple c# replace
    ///     18-5-2023
    ///     Nevím zda se tato metoda měnila. Ale měl jsem u jejího volání přehozené A2,3
    ///     Zatím to nechám jak je.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="what"></param>
    /// <param name="forWhat"></param>
    public static string Replace(string t, string what, string forWhat, bool a2CanBeAsA3 = false)
    {
        if (what == forWhat)
        {
            if (a2CanBeAsA3)
                return t;
            ThrowEx.IsTheSame("what", "forWhat");
        }

        var r = t.Replace(what, forWhat);
        return r;
    }

    public static string ReplaceLastOccurenceOfString(string text, string co, string čím)
    {
        var roz = SHSplit.SplitMore(text, co);
        if (roz.Count == 1) return text.Replace(co, čím);

        var sb = new StringBuilder();
        for (var i = 0; i < roz.Count - 2; i++) sb.Append(roz[i] + co);
        sb.Append(roz[roz.Count - 2]);
        sb.Append(čím);
        sb.Append(roz[roz.Count - 1]);
        return sb.ToString();
    }

    public static string ReplaceFirstOccurences(string v, string zaCo, string co, char maxToFirstChar)
    {
        var dexCo = v.IndexOf(co);
        if (dexCo == -1) return v;
        var dex = v.IndexOf(maxToFirstChar);
        if (dex == -1) dex = v.Length;
        if (dexCo > dex) return v;
        return ReplaceOnce(v, co, zaCo);
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

    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string vcem2, string co, string zaCo,
        int overallCountOfA2)
    {
        var r = new Regex(co);
        //StringBuilder vcem = new StringBuilder(vcem2);
        var dex = vcem2.IndexOf(co);
        if (dex != -1) return r.Replace(vcem2, zaCo, int.MaxValue, dex + co.Length);
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

        var p = SHSplit.SplitMore(text, whitespaceChar.whiteSpaceChars.ConvertAll(d => d.ToString()).ToArray());
        return string.Join(" ", p);
    }

    public static string ReplaceWhiteSpacesExcludeSpaces(string p)
    {
        return p.Replace("\r", "").Replace("\n", "").Replace("\t", "");
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
    public static string ReplaceWhiteSpaces(string p, string replaceWith)
    {
        var replaced = ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(p, replaceWith);
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
    public static string ReplaceManyFromString(string input, string v, string delimiter)
    {
        var methodName = "ReplaceManyFromString";
        var l = SHGetLines.GetLines(v);
        foreach (var item in l)
        {
            var p = SHSplit.SplitMore(item, delimiter);
            p = p.ConvertAll(d => d.Trim());
            string from, to;
            from = to = null;
            if (p.Count > 0)
                from = p[0];
            else
                throw new Exception(item + " hasn't from");
            if (p.Count > 1)
                to = p[1];
            else
                throw new Exception(item + " hasn't to");
            if (WildcardHelper.IsWildcard(item))
            {
                var wc = new Wildcard(from);
                ThrowEx.NotImplementedMethod();
                //var occurences = wc.Matches(input);
                //foreach (Match m in occurences)
                //{
                //    var result = m.Result();
                //    var groups = m.Groups;
                //    var captues = m.Captures;
                //    var value = m.Value;
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
        var l = SHGetLines.GetLines(result);
        if (l[0] == from)
            l[0] = to;
        else
            throw new Exception($"First line is not excepted '{from}', was '{l[0]}'");
        return string.Join("\n", l);
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
                // všechny elementy z contentOneSpace namapované na content kde v něm začínají.
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

    public static string ReplaceWithIndex(string n, string v, string empty, ref int dx)
    {
        if (dx == -1)
        {
            dx = n.IndexOf(v);
            if (dx != -1)
            {
                n = n.Remove(dx, v.Length);
                n = n.Insert(dx, empty);
            }
        }

        return n;
    }

    public static string ReplaceTypedWhitespacesForNormal(string t, bool quote, bool t24, bool bs)
    {
        sb.Clear();
        t = t.Trim();
        // jen zde protože jestli něco dělám přes ts tak to dělám na rychlost a to už musí být tohohle zbavené
        t = t.TrimEnd('"', '\'');
        sb.Append(t);
        return ReplaceTypedWhitespacesForNormal(sb, quote, t24, bs).ToString();
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

    public static string ReplaceInLine(string l, string what, string to, bool checkForMoreOccurences)
    {
        var c = new List<string>(new[] { l }); //CAG.ToList(l);
        ReplaceInLine(c, 1, what, to, checkForMoreOccurences);
        return c[0];
    }

    public static void ReplaceInLine(List<string> l, int lineFromOne, string what, string to,
        bool checkForMoreOccurences)
    {
        if (checkForMoreOccurences)
        {
            var occ = SH.ReturnOccurencesOfString(l[lineFromOne - 1], what);
            if (occ.Count > 1)
                foreach (var item in occ)
                {
                    var after = l[lineFromOne - 1][item + what.Length];
                    if (after == ',' || after == ' ')
                    {
                        var s = l[lineFromOne - 1];
                        s = s.Remove(item, what.Length);
                        s = s.Insert(item, to);
                        l[lineFromOne - 1] = s;
                        break;
                    }
                }
            else
                l[lineFromOne - 1] = ReplaceOnce(l[lineFromOne - 1], what, to);
        }
        else
        {
            l[lineFromOne - 1] = ReplaceOnce(l[lineFromOne - 1], what, to);
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