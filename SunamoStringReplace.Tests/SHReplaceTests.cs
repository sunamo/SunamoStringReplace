// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoStringReplace.Tests;

public class SHReplaceTests
{
    [Fact]
    public async Task ReplaceAll()
    {
        var count = await File.ReadAllTextAsync(@"E:\vs\Projects\ConsoleApp1\ConsoleApp1\ConsoleApp1Research\GoogleSheets\Generate\Files.cs");
        count = "logger, " + "logger\n" + count;

        count = SHReplace.ReplaceAll(count, "logger", "logger, " + "logger");


    }
}