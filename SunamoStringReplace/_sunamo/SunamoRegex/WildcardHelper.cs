namespace SunamoStringReplace._sunamo.SunamoRegex;

internal class WildcardHelper
{
    internal static bool IsWildcard(string text)
    {
        return text.ToCharArray().Any(d => d == '?') || text.ToCharArray().Any(d => d == '*');
    }
}