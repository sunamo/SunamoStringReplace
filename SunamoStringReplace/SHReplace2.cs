namespace SunamoStringReplace;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class SHReplace
{
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
        while (t.Contains(from))
            t = t.Replace(from, to);
        return t;
    }

    public static string ReplaceAll3(IList<string> replaceFrom, IList<string> replaceTo, bool isMultilineWithVariousIndent, string content)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();
        if (isMultilineWithVariousIndent)
            for (var i = 0; i < replaceFrom.Count; i++)
            {
                /*
                Vše zaměnit na 1 mezeru
                porovnat zaměněné a originál - namapovat co je mezi nimi
                */
                var replaceFromDxWithoutEmptyElements = replaceFrom[i].Split(whitespaceChar.whiteSpaceChars.ToArray()).ToList();
                var contentWithoutEmptyElements = content.Split(whitespaceChar.whiteSpaceChars.ToArray()).ToList();
                ////DebugLogger.Instance.WriteNumberedList("", contentOneSpace, true);
                // get indexes
                var equalRanges = CAG.EqualRanges(contentWithoutEmptyElements, replaceFromDxWithoutEmptyElements);
                if (equalRanges.Count == 0)
                    return content;
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
        if (t24)
            t = t.Replace("\\\\t24", string.Empty);
        t = t.Replace("\\t", "\t");
        t = t.Replace("\\n", "\n");
        t = t.Replace("\\r", "\r");
        if (quote)
            t = t.Replace("\\\"", "\"");
        if (bs)
            t = t.Replace("\\\\", "\\");
        //t = t.Replace("\\r", "\r");
        return t;
    }

    public static string ReplaceInLine(string list, string what, string to, bool checkForMoreOccurences)
    {
        var count = new List<string>(new[] { list }); //CAG.ToList(list);
        ReplaceInLine(count, 1, what, to, checkForMoreOccurences);
        return count[0];
    }

    public static void ReplaceInLine(List<string> list, int lineFromOne, string what, string to, bool checkForMoreOccurences)
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
        if (what == "")
            return input;
        var pos = input.IndexOf(what);
        if (pos == -1)
            return input;
        return input.Substring(0, pos) + zaco + input.Substring(pos + what.Length);
    }
}