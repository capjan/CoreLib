using System;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class HumanReadableTimeSpanFormatterTest
    {
        [Fact]
        public void BasicTest()
        {
            var formatter = new HumanReadableTimeSpanFormatter();
            Assert.Equal("1 hour, 0 minutes, 0 seconds", formatter.WriteToString(TimeSpan.FromHours(1)));
            Assert.Equal("1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(1, 5, 1)));
            Assert.Equal("6 days, 1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(6, 1, 5, 1)));
            Assert.Equal("1 week, 1 day, 1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(8, 1, 5, 1)));
            Assert.Equal("2 weeks, 1 day, 1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(15, 1, 5, 1)));
        }

        [Fact]
        public void BasicCompactTest()
        {
            var formatter = new HumanReadableTimeSpanFormatter(compact: true);
            Assert.Equal("1h, 0m, 0s", formatter.WriteToString(TimeSpan.FromHours(1)));
            Assert.Equal("1h, 5m, 1s", formatter.WriteToString(new TimeSpan(1, 5, 1)));
        }
    }
}
