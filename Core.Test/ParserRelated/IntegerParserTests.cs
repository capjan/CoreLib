using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated
{

    public class IntegerParserTests
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(123, "123")]
        [InlineData(-1794, "-1794")]
        [InlineData(0, "WTF!")]
        public void BasicTest(int expected, string input)
        {
            var sut = new IntegerParser();
            Assert.Equal(expected, sut.ParseOrFallback(input));
        }

    }
}
