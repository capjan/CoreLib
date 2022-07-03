using System;
using System.Globalization;
using Core.Enums;
using Core.Text.Formatter;
using Core.Extensions.TextRelated;
using Xunit;

namespace Core.Test.TextRelated;

public class FileSizeFormatterTest
{
    [Fact]
    public void BasicTest()
    {
        IFileSizeFormatter formatter = new FileSizeFormatter();
        var result = formatter.WriteToString(1024);
        Assert.Equal("1 KiB", result);
    }
    
    [Fact]
    public void BasicStraightFormatter()
    {
        IFileSizeFormatter formatter = new FileSizeFormatter();
        formatter.ForcedUnit = BinaryUnitPrefix.None;
        var result = formatter.WriteToString(1024);
        Assert.Equal("1024 B", result);
    }
    
    [Fact]
    public void BigTest()
    {
        var formatter = new FileSizeFormatter
        {
            Format = "0.##",
            FormatProvider = CultureInfo.InvariantCulture
        };

        var fileSize = 123456789L;
        
        Assert.Equal("117.74 MiB", formatter.WriteToString(fileSize));
        formatter.ForcedUnit = BinaryUnitPrefix.Kibi;
        Assert.Equal("120563.27 KiB", formatter.WriteToString(fileSize));
        formatter.ForcedUnit = BinaryUnitPrefix.None;
        Assert.Equal("123456789 B", formatter.WriteToString(fileSize));
    }
}