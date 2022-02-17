using Core.Extensions.ParserRelated;
using Core.Parser.Basic;
using Xunit;
// ReSharper disable StringLiteralTypo

namespace Core.Test.ParserRelated;

public class BoolParserTests
{
    [Fact]
    public void BasicTests()
    {
        var parser = new BoolParser();

        // must evaluate to true
        Assert.True(parser.ParseOrDefault("true"));
        Assert.True(parser.ParseOrDefault("t"));
        Assert.True(parser.ParseOrDefault("yes"));
        Assert.True(parser.ParseOrDefault("ja"));
        Assert.True(parser.ParseOrDefault("1"));
        Assert.True(parser.ParseOrDefault("y"));
        Assert.True(parser.ParseOrDefault("j"));
        Assert.True(parser.ParseOrFallback("", true));

        // must evaluate to false
        Assert.False(parser.ParseOrDefault("false"));
        Assert.False(parser.ParseOrDefault("False"));
        Assert.False(parser.ParseOrDefault("FALSE"));
        Assert.False(parser.ParseOrDefault("F"));
        Assert.False(parser.ParseOrDefault("No"));
        Assert.False(parser.ParseOrDefault("Nein"));
        Assert.False(parser.ParseOrDefault("N"));
        Assert.False(parser.ParseOrDefault(""));

    }

    public static int StringToInt(string value)
    {
        return 1234;
    }
}