namespace SunamoStringReplace;


internal class SHGetLines
{
    internal static List<string> GetLines(string p)
    {
        List<string> vr = new List<string>();
        StringReader sr = new StringReader(p);
        string f = null;
        while ((f = sr.ReadLine()) != null)
        {
            vr.Add(f);
        }
        return vr;
    }
}