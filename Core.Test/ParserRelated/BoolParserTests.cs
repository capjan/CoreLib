using Core.Parser.Basic;
using Xunit;
// ReSharper disable StringLiteralTypo

namespace Core.Test.ParserRelated
{
    public class BoolParserTests
    {
        [Fact]
        public void BasicTests()
        {
            var parser = new BoolParser();

            // must evaluate to true
            Assert.True(parser.ParseOrFallback("true", default));
            Assert.True(parser.ParseOrFallback("t", default));
            Assert.True(parser.ParseOrFallback("yes",default));
            Assert.True(parser.ParseOrFallback("ja",default));
            Assert.True(parser.ParseOrFallback("1",default));
            Assert.True(parser.ParseOrFallback("y",default));
            Assert.True(parser.ParseOrFallback("j",default));
            Assert.True(parser.ParseOrFallback("", true));

            // must evaluate to false
            Assert.False(parser.ParseOrFallback("false", default));
            Assert.False(parser.ParseOrFallback("False", default));
            Assert.False(parser.ParseOrFallback("FALSE", default));
            Assert.False(parser.ParseOrFallback("F", default));
            Assert.False(parser.ParseOrFallback("No", default));
            Assert.False(parser.ParseOrFallback("Nein", default));
            Assert.False(parser.ParseOrFallback("N", default));
            Assert.False(parser.ParseOrFallback("", default));

        }

        public static int StringToInt(string value)
        {
            return 1234;
        }
    }
}
