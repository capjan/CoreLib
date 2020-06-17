using System;
using Core.Parser.Impl;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class DateTimeParserTest
    {
        [Fact]
        public void BasicTest()
        {
            var parser = new DateTimeParser();
            Assert.Equal(new DateTime(2020,06,17, 1,2,3), parser.ParseOrFallback("2020.06.17T01:02:03.000Z"));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrFallback("2020-06-17T01:02:03.123Z"));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrFallback("2020-06-17 01:02:03.123Z"));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 100), parser.ParseOrFallback("2020-06-17 01:02:03.1Z"));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 210), parser.ParseOrFallback("2020-06-17 01:02:03.21Z"));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3), parser.ParseOrFallback("2020-06-17 01:02:03Z"));
            Assert.Equal(new DateTime(2020,06,17, 1,2,0), parser.ParseOrFallback("2020.06.17 - 01:02 Z"));
        }
    }
}
