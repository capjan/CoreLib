using Core.Parser.Special;
using Core.Parser.TokenParser;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class WordTokenParserTest
    {

        [Fact]
        public void BasicTest()
        {
            using (var input = ParserInput.CreateFromString("Hello World"))
            {
                var parser = new WordTokenParser();
                Assert.Equal("Hello", parser.Parse(input));
                input.TryReadChar(out _);
                Assert.Equal("World", parser.Parse(input));
            }
        }
    }
}
