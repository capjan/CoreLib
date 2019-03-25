using System;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class DateTimeFormatterTest
    {
        [Fact]
        public void BasicTest()
        {            
            var utcDateTime = new DateTime(1980, 8, 1, 10,15,0, DateTimeKind.Utc);
            var formatter = new DefaultDateTimeFormatter(localTime: false);
            Assert.Equal("01.08.1980 10:15:00.000", formatter.FormatToString(utcDateTime));              
        }
    }
}
