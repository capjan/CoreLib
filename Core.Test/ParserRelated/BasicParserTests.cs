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
            Assert.Equal(234, parser.ParseOrFallback("", 234));
            Assert.Equal(567, parser.ParseOrFallback("wrong input", 567));
        }

        [Fact]
        public void TestIntArrayParser()
        {
            var parser = new IntArrayParser();
            Assert.Equal(new [] {1,2,3}, parser.ParseOrFallback("1,2,3"));
            Assert.Equal(new [] {4,5,6}, parser.ParseOrFallback("4, 5,6"));
            Assert.Equal(new [] {7,8,9}, parser.ParseOrFallback("", new []{7,8,9}));
            Assert.Null(parser.ParseOrFallback(""));
            Assert.Null(parser.ParseOrFallback("1,2,3,kill"));
        }
    }
}
