namespace SunamoStringReplace.Tests;

public class SHReplaceTests
{
    [Fact]
    public async Task ReplaceAll()
    {
        var c = await File.ReadAllTextAsync(@"E:\vs\Projects\ConsoleApp1\ConsoleApp1\ConsoleApp1Research\GoogleSheets\Generate\Files.cs");
        c = "logger, " + "logger\n" + c;

        c = SHReplace.ReplaceAll(c, "logger", "logger, " + "logger");


    }
}