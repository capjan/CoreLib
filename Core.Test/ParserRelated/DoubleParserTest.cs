using Core.Extensions.ParserRelated;
using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated;

public class DoubleParserTest
{
    [Fact]
    public void BasicDoubleParserTest()
    {
        var parser = new DoubleParser();
        Assert.Equal(1.234, parser.Parse("1.234"));
        Assert.Equal(1.234, parser.Parse("1,234"));
        Assert.Equal(1234.0, parser.Parse("1234"));
        Assert.Equal(123.45, parser.Parse("123,45"));
        Assert.Equal(1234.56, parser.Parse("1.234,56"));
        Assert.Equal(1234.56, parser.Parse("1,234.56"));
        Assert.Equal(0.0, parser.ParseOrFallback("0", default));
        Assert.Equal(-123.45, parser.Parse("-123.45"));
        Assert.Equal(-123.45, parser.Parse("-123,45"));
        Assert.Equal(-1234.5, parser.Parse("-1,234.5"));
        Assert.Equal(-1234.5, parser.Parse("-1.234,5"));
    }
}