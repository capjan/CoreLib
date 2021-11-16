using System;
using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class DateTimeParserTest
    {
        [Fact]
        public void BasicTest()
        {
            var parser = new DateTimeParser();
            Assert.Equal(new DateTime(2020,06,17, 1,2,3), parser.ParseOrFallback("2020.06.17T01:02:03.000Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrFallback("2020-06-17T01:02:03.123Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrFallback("2020-06-17 01:02:03.123Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 100), parser.ParseOrFallback("2020-06-17 01:02:03.1Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 210), parser.ParseOrFallback("2020-06-17 01:02:03.21Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3), parser.ParseOrFallback("2020-06-17 01:02:03Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,0), parser.ParseOrFallback("2020.06.17 - 01:02 Z", default));
            Assert.Equal(new DateTime(2020,06,17, 1,2,3, 123), parser.ParseOrFallback("2020-06-17 01:02:03.123456", default));
            Assert.Equal(new DateTime(1976,7,10), parser.ParseOrFallback("", new DateTime(1976,7,10)));
        }
    }
}
