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
}