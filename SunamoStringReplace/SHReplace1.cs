// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringReplace;
public partial class SHReplace
{
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
                if (prefix != fromCannotBePrefixed)
                    t = ReplaceByIndex(t, to, item, from.Length);
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
    /// <param name = "vstup"></param>
    /// <param name = "zaCo"></param>
    /// <param name = "co"></param>
    public static string ReplaceAll2(string vstup, string zaCo, string co)
    {
        return vstup.Replace(co, zaCo);
    }

    /// <summary>
    ///     Protože jsem zapomínal na to že jsem ReplaceAll nahradil za ReplaceAllArray dávám tu zpět i tento starý název,
    ///     dokud zase nebude někomu vadit
    /// </summary>
    /// <param name = "vstup"></param>
    /// <param name = "zaCo"></param>
    /// <param name = "co"></param>
    /// <returns></returns>
    public static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {
        return ReplaceAllArray(vstup, zaCo, co);
    }

    /// <summary>
    ///     If you want to replace multiline content with various indent use ReplaceAllDoubleSpaceToSingle2 to every variable
    ///     which you are passed
    /// </summary>
    /// <param name = "vstup"></param>
    /// <param name = "zaCo"></param>
    /// <param name = "co"></param>
    public static string ReplaceAllArray(string vstup, string zaCo, params string[] co)
    {
        foreach (var item in co)
            if (string.IsNullOrEmpty(item))
                return vstup;
        foreach (var item in co)
            vstup = vstup.Replace(item, zaCo);
        return vstup;
    }

    /// <summary>
    ///     Use simple count# replace
    ///     18-5-2023
    ///     Nevím zda se tato metoda měnila. Ale měl jsem u jejího volání přehozené A2,3
    ///     Zatím to nechám jak je.
    /// </summary>
    /// <param name = "t"></param>
    /// <param name = "what"></param>
    /// <param name = "forWhat"></param>
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
        if (roz.Count == 1)
            return text.Replace(co, čím);
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < roz.Count - 2; i++)
            stringBuilder.Append(roz[i] + co);
        stringBuilder.Append(roz[roz.Count - 2]);
        stringBuilder.Append(čím);
        stringBuilder.Append(roz[roz.Count - 1]);
        return stringBuilder.ToString();
    }

    public static string ReplaceFirstOccurences(string value, string zaCo, string co, char maxToFirstChar)
    {
        var dexCo = value.IndexOf(co);
        if (dexCo == -1)
            return value;
        var dex = value.IndexOf(maxToFirstChar);
        if (dex == -1)
            dex = value.Length;
        if (dexCo > dex)
            return value;
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

    public static string ReplaceSecondAndNextOccurencesOfStringFrom(string vcem2, string co, string zaCo /*,
        int overallCountOfA2*/)
    {
        var result = new Regex(co);
        //StringBuilder vcem = new StringBuilder(vcem2);
        var dex = vcem2.IndexOf(co);
        if (dex != -1)
            return result.Replace(vcem2, zaCo, int.MaxValue, dex + co.Length);
        //return vcem.Replace(co, zaCo, dex + co.Length , overallCountOfA2 - 1 ).ToString();
        return vcem2;
    }

    /// <summary>
    ///     Working - see unit tests
    ///     Split by all whitespaces - remove also newline
    ///     ReplaceAllDoubleSpaceToSingle not working correctly while copy from webpage
    ///     Split and join again
    /// </summary>
    /// <param name = "text"></param>
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
                throw new Exception("Nahrazovan\u00FD prvek " + item + " je prvkem j\u00EDm\u017E se nahrazuje  " + zaCo + ".");
        for (var i = 0; i < co.Length; i++)
            vr = Regex.Replace(vr, co[i], zaCo, RegexOptions.IgnoreCase);
        return vr;
    }

    /// <summary>
    ///     Replace every whitespace with empty string
    /// </summary>
    /// <param name = "replaceWith"></param>
    public static string ReplaceWhiteSpaces(string replaceWith)
    {
        return ReplaceWhiteSpaces(replaceWith, "");
    }

    /// <summary>
    ///     Replace all whitespaces except of space and then A1 with space
    ///     In other words, in finale, replace every whitespace with space - A2 is for better customizing
    ///     A2 can be also space
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "replaceWith"></param>
    public static string ReplaceWhiteSpaces(string parameter, string replaceWith)
    {
        var replaced = ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(parameter, replaceWith);
        return Replace(replaced, "", replaceWith, true);
    }

    /// <summary>
    ///     A2 a->b
    ///     A3 ->
    /// </summary>
    /// <param name = "input"></param>
    /// <param name = "v"></param>
    /// <param name = "delimiter"></param>
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
}