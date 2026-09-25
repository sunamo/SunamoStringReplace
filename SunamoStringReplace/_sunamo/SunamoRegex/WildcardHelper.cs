namespace SunamoStringReplace._sunamo.SunamoRegex;

internal class WildcardHelper
{
    internal static bool IsWildcard(string text)
    {
        return text.Any(character => character == '?') || text.Any(character => character == '*');
    }
}
