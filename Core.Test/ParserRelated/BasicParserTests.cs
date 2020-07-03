using Core.Parser.Basic;
using Core.Parser.Special;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class BasicParserTests
    {
        [Fact]
        public void TestIntegerParser()
        {
            var parser = new OptionalIntParser();
            Assert.Equal(123, parser.ParseOrFallback("123"));
            Assert.Equal(234, parser.ParseOrFallback(null, 234));
            Assert.Equal(567, parser.ParseOrFallback("blabla", 567));
        }

        [Fact]
        public void TestIntArrayParser()
        {
            var parser = new IntArrayParser();
            Assert.Equal(new int[] {1,2,3}, parser.ParseOrFallback("1,2,3"));
            Assert.Equal(new int[] {4,5,6}, parser.ParseOrFallback("4, 5,6"));
            Assert.Equal(new int[] {7,8,9}, parser.ParseOrFallback(null, new []{7,8,9}));
            Assert.Null(parser.ParseOrFallback(null));
            Assert.Null(parser.ParseOrFallback("1,2,3,kill"));
        }
    }
}
