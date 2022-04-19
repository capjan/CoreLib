using System;
using Core.Extensions.ParserRelated;
using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated;

public class DateTimeParserTest
{
    [Fact]
    public void BasicTest()
    {
        var parser = new DateTimeParser();
        Assert.Equal(new DateTime(2020,06,17, 1,2,3), parser.ParseOrDefault("2020.06.17T01:02:03.000Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrDefault("2020-06-17T01:02:03.123Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrDefault("2020-06-17 01:02:03.123Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,3, 100), parser.ParseOrDefault("2020-06-17 01:02:03.1Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,3, 210), parser.ParseOrDefault("2020-06-17 01:02:03.21Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,3), parser.ParseOrDefault("2020-06-17 01:02:03Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,0), parser.ParseOrDefault("2020.06.17 - 01:02 Z"));
        Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrDefault("2020-06-17 01:02:03.123456"));
        Assert.Equal(new DateTime(1976,7,10), parser.ParseOrFallback("", new DateTime(1976,7,10)));
    }

    [Fact]
    public void ParseOptionalDatetime()
    {
        var sut = new DateTimeParser();

        var result = sut.ParseOrNull(null);
        Assert.False(result.HasValue);
    }
}