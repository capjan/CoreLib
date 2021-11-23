using Core.Extensions.ParserRelated;
using Core.Parser;
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
            var parser = new IntegerParser();
            Assert.Equal(123, parser.ParseOrDefault("123"));
            Assert.Equal(234, parser.ParseOrFallback("", 234));
            Assert.Equal(567, parser.ParseOrFallback("wrong input", 567));
        }

        [Fact]
        public void TestIntArrayParser()
        {
            var parser = new IntegerParser();
            Assert.Equal(new [] {1,2,3}, parser.ParseList("1,2,3"));
            Assert.Equal(new [] {4,5,6}, parser.ParseList("4, 5,6"));
            Assert.Equal(new [] {7,8,9}, parser.ParseListOrFallback("", new []{7,8,9}));
            Assert.Null(parser.ParseListOrNull(""));
            Assert.Null(parser.ParseListOrNull("1,2,3,kill"));
        }
    }
}
