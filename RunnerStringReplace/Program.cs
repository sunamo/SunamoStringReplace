using SunamoStringReplace.Tests;

namespace RunnerStringReplace;

/// <summary>
/// Entry point for running string replace tests.
/// </summary>
internal class Program
{
    static void Main()
    {
        SHReplaceTests t = new();
        t.ReplaceAll();
    }
}
