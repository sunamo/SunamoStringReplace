namespace SunamoStringReplace._sunamo.SunamoRegex;

/// <summary>
/// Provides helper methods for wildcard pattern detection.
/// </summary>
internal class WildcardHelper
{
    /// <summary>
    /// Determines whether the text contains wildcard characters (? or *).
    /// </summary>
    /// <param name="text">The text to check for wildcard characters.</param>
    /// <returns>True if the text contains wildcard characters, false otherwise.</returns>
    internal static bool IsWildcard(string text)
    {
        return text.Any(character => character == '?') || text.Any(character => character == '*');
    }
}