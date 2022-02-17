using System.Linq;
using Core.Extensions.ParserRelated;
using Core.Parser;
using Core.Parser.Basic;
using Core.Parser.Special;
using Xunit;

namespace Core.Test.ParserRelated;

public class BasicParserTests
{
    [Fact]
    public void TestIntegerParser()
    {
        var parser = new IntegerParser();
        Assert.Equal(123, parser.ParseOrDefault("123"));
        Assert.Equal(234, parser.ParseOrFallback("", 234));
        Assert.Equal(567, parser.ParseOrFallback("wrong input", 567));
    }

    [Fact]
    public void TestIntArrayParser()
    {
        var parser = new IntegerParser();
        Assert.Equal(new [] {1,2,3}, parser.ParseToArray("1,2,3"));
        Assert.Equal(new [] {4,5,6}, parser.ParseToArray("4, 5,6"));
        Assert.Equal(new [] {7,8,9}, parser.ParseToArrayOrFallback("", new []{7,8,9}).ToArray());
        Assert.Null(parser.ParseToArrayOrNull(""));
        Assert.Null(parser.ParseToArrayOrNull("1,2,3,kill"));
    }
}