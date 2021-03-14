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
            Assert.True(parser.ParseOrFallback("true"));
            Assert.True(parser.ParseOrFallback("t"));
            Assert.True(parser.ParseOrFallback("yes"));
            Assert.True(parser.ParseOrFallback("ja"));
            Assert.True(parser.ParseOrFallback("1"));
            Assert.True(parser.ParseOrFallback("y"));
            Assert.True(parser.ParseOrFallback("j"));
            Assert.True(parser.ParseOrFallback(null, true));

            // must evaluate to false
            Assert.False(parser.ParseOrFallback("false"));
            Assert.False(parser.ParseOrFallback("False"));
            Assert.False(parser.ParseOrFallback("FALSE"));
            Assert.False(parser.ParseOrFallback("F"));
            Assert.False(parser.ParseOrFallback("No"));
            Assert.False(parser.ParseOrFallback("Nein"));
            Assert.False(parser.ParseOrFallback("N"));
            Assert.False(parser.ParseOrFallback(null));

        }

        public static int StringToInt(string value)
        {
            return 1234;
        }
    }
}
