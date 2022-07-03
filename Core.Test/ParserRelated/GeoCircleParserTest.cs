using System;
using Core.Extensions.ParserRelated;
using Core.Parser.Special;
using Xunit;

namespace Core.Test.ParserRelated;

public class GeoCircleParserTest
{
    [Fact]
    public void BasicTest()
    {
        var sut = new GeoCircleParser();
        var result = sut.ParseOrDefault(string.Empty);
        Assert.Null(result);
    }
}