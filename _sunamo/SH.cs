namespace SunamoStringReplace._sunamo;
internal class SH
{
    internal static Func<string, int, string, string, string> RemoveAndInsertReplace;
    internal static Func<string, bool, List<string>> SplitByWhiteSpaces;
    internal static Func<string, string, List<int>> ReturnOccurencesOfString;
    internal static Func<string, string> FromSpace160To32;
    internal static Func<string, string, string, string> RemoveEndingPairCharsWhenDontHaveStarting;
}
