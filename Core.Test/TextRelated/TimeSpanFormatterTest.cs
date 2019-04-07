using System;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class TimeSpanFormatterTest
    {
        [Fact]
        public void BasicTest()
        {
            var formatter = new DefaultTimeSpanFormatter();
            Assert.Equal("1 hour, 0 minutes, 0 seconds", formatter.WriteToString(TimeSpan.FromHours(1)));
            Assert.Equal("1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(1, 5, 1)));
            Assert.Equal("6 days, 1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(6, 1, 5, 1)));
            Assert.Equal("1 week, 1 day, 1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(8, 1, 5, 1)));
            Assert.Equal("2 weeks, 1 day, 1 hour, 5 minutes, 1 second", formatter.WriteToString(new TimeSpan(15, 1, 5, 1)));
        }

        [Fact]
        public void BasicCompactTest()
        {
            var formatter = new DefaultTimeSpanFormatter(compact: true);
            Assert.Equal("1h, 0m, 0s", formatter.WriteToString(TimeSpan.FromHours(1)));
            Assert.Equal("1h, 5m, 1s", formatter.WriteToString(new TimeSpan(1, 5, 1)));
        }

        [Fact]
        public void BasicCustomFormattingTest()
        {
            var formatter = new DefaultTimeSpanFormatter {CustomFormat = @"d\.hh\:mm\:ss\.fff"};
            Assert.Equal("0.01:00:00.000", formatter.WriteToString(TimeSpan.FromHours(1)));
            Assert.Equal("0.01:05:01.000", formatter.WriteToString(new TimeSpan(1, 5, 1)));
        }
    }
}
