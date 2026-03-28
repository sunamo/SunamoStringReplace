namespace SunamoStringReplace.Tests;

/// <summary>
/// Unit tests for the SHReplace class.
/// </summary>
public class SHReplaceTests
{
    /// <summary>
    /// Tests that ReplaceAll correctly replaces all occurrences of a search string.
    /// </summary>
    [Fact]
    public void ReplaceAll()
    {
        var text = "Hello world, hello universe";
        var result = SHReplace.ReplaceAll(text, "hi", "Hello", "hello");
        Assert.Equal("hi world, hi universe", result);
    }

    /// <summary>
    /// Tests that ReplaceAll2 correctly replaces a single search string.
    /// </summary>
    [Fact]
    public void ReplaceAll2()
    {
        var text = "foo bar foo";
        var result = SHReplace.ReplaceAll2(text, "baz", "foo");
        Assert.Equal("baz bar baz", result);
    }

    /// <summary>
    /// Tests that ReplaceOnce replaces only the first occurrence.
    /// </summary>
    [Fact]
    public void ReplaceOnce()
    {
        var text = "aaa bbb aaa";
        var result = SHReplace.ReplaceOnce(text, "aaa", "ccc");
        Assert.Equal("ccc bbb aaa", result);
    }

    /// <summary>
    /// Tests that ReplaceAllDoubleSpaceToSingle normalizes double spaces.
    /// </summary>
    [Fact]
    public void ReplaceAllDoubleSpaceToSingle()
    {
        var text = "Hello  World   Test";
        var result = SHReplace.ReplaceAllDoubleSpaceToSingle(text);
        Assert.Equal("Hello World Test", result);
    }

    /// <summary>
    /// Tests that ReplaceByIndex correctly replaces a substring at a specific index.
    /// </summary>
    [Fact]
    public void ReplaceByIndex()
    {
        var text = "Hello World";
        var result = SHReplace.ReplaceByIndex(text, "Universe", 6, 5);
        Assert.Equal("Hello Universe", result);
    }

    /// <summary>
    /// Tests that ReplaceWhiteSpacesExcludeSpaces removes non-space whitespace.
    /// </summary>
    [Fact]
    public void ReplaceWhiteSpacesExcludeSpaces()
    {
        var text = "Hello\r\n\tWorld";
        var result = SHReplace.ReplaceWhiteSpacesExcludeSpaces(text);
        Assert.Equal("HelloWorld", result);
    }

    /// <summary>
    /// Tests that ReplaceAllCaseInsensitive replaces regardless of case.
    /// </summary>
    [Fact]
    public void ReplaceAllCaseInsensitive()
    {
        var text = "Hello HELLO hello";
        var result = SHReplace.ReplaceAllCaseInsensitive(text, "hi", "hello");
        Assert.Equal("hi hi hi", result);
    }
}
