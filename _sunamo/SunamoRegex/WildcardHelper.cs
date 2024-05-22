namespace SunamoStringReplace;


internal class WildcardHelper
{
    internal static bool IsWildcard(string text)
    {
        return text.ToCharArray().Any(d => d == AllChars.q) || text.ToCharArray().Any(d => d == AllChars.asterisk);
    }
}