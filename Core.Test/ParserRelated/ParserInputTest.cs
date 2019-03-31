using Core.Parser.Impl;
using Core.Text.Impl;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class ParserInputTest
    {
        [Fact]
        public void BasicStringInputTest()
        {
            const string source = "abcdef 12345";
            using (var input = ParserInput.CreateFromString(source))
            {
                Assert.Equal(0, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                var pos1 = new TextPosition();

                // lookahead 1
                Assert.Equal(pos1, input.TextPosition);
                Assert.True(input.TryPeekChar(out var ch));
                Assert.Equal('a', ch);
                Assert.Equal(pos1, input.TextPosition);
                Assert.Equal(1, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                // lookahead 2
                Assert.True(input.TryPeekChar(out ch));
                Assert.Equal('b', ch);
                Assert.Equal(pos1, input.TextPosition);
                Assert.Equal(2, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                // read the first char. This should reset the lookup
                Assert.True(input.TryReadChar(out ch));
                Assert.Equal('a', ch);
                Assert.Equal(1, input.TextPosition.LineNumber);
                Assert.Equal(2, input.TextPosition.ColumnNumber);
                Assert.Equal(0, input.LookaheadCount);
                Assert.Equal(1, input.Offset);

                // lookahead 1
                Assert.True(input.TryPeekChar(out ch));
                Assert.Equal('b', ch);
                Assert.Equal(1, input.TextPosition.LineNumber);
                Assert.Equal(2, input.TextPosition.ColumnNumber);
                Assert.Equal(1, input.LookaheadCount);
                Assert.Equal(1, input.Offset);

                // read next char
                Assert.True(input.TryReadChar(out ch));
                Assert.Equal('b', ch);
                Assert.Equal(1, input.TextPosition.LineNumber);
                Assert.Equal(3, input.TextPosition.ColumnNumber);
                Assert.Equal(0, input.LookaheadCount);
                Assert.Equal(2, input.Offset);

            }
        }

         [Fact]
        public void BasicLookupTest()
        {
            const string source = "abcdef 12345";
            using (var input = ParserInput.CreateFromString(source))
            {
                Assert.Equal(0, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                var pos1 = new TextPosition();

                // lookahead 1
                Assert.Equal(pos1, input.TextPosition);
                Assert.True(input.TryPeekChar(out var ch));
                Assert.Equal('a', ch);
                Assert.Equal(pos1, input.TextPosition);
                Assert.Equal(1, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                // lookahead 2
                Assert.True(input.TryPeekChar(out ch));
                Assert.Equal('b', ch);
                Assert.Equal(pos1, input.TextPosition);
                Assert.Equal(2, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                input.ClearLookahead();
                Assert.Equal(0, input.LookaheadCount);
                Assert.Equal(pos1, input.TextPosition);

                // lookahead 1
                Assert.Equal(pos1, input.TextPosition);
                Assert.True(input.TryPeekChar(out ch));
                Assert.Equal('a', ch);
                Assert.Equal(pos1, input.TextPosition);
                Assert.Equal(1, input.LookaheadCount);
                Assert.Equal(0, input.Offset);

                // lookahead 2
                Assert.True(input.TryPeekChar(out ch));
                Assert.Equal('b', ch);
                Assert.Equal(pos1, input.TextPosition);
                Assert.Equal(2, input.LookaheadCount);
                Assert.Equal(0, input.Offset);
            }
        }
    }
}
