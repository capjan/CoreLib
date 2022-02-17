using System;
using System.Globalization;
using Core.Extensions.TextRelated;
using Core.Text.Formatter;
using Xunit;

namespace Core.Test.TextRelated;

public class DateTimeFormatterTest
{
    [Fact]
    public void BasicTest()
    {            
        var utcDateTime = new DateTime(1980, 8, 1, 10,15,0, DateTimeKind.Utc);
        var formatter = new DateTimeFormatter(universalTime: true);
        Assert.Equal("01.08.1980 10:15:00.000", formatter.WriteToString(utcDateTime));

        formatter.FormatProvider = CultureInfo.InvariantCulture;
        formatter.Format = "f";
        Assert.Equal("Friday, 01 August 1980 10:15", formatter.WriteToString(utcDateTime));

        formatter.Format = "HH:ss";
        Assert.Matches(@"\d{2}:\d{2}", formatter.WriteToString());
    }
}