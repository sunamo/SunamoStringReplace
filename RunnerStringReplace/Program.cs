using SunamoStringReplace.Tests;

namespace RunnerStringReplace;

internal class Program
{
    static async Task Main()
    {
        SHReplaceTests t = new();
        await t.ReplaceAll();
    }
}
