using Core.Extensions.TextRelated;
using Core.Text;
using Core.Text.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class TextPositionTest
    {

        [Fact]
        public void BasicTextPositionTest()
        {
            var pos1 = TextPosition.Start;
            Assert.Equal(1,pos1.LineNumber);
            Assert.Equal(1, pos1.ColumnNumber);

            var pos2 = pos1.NextColumn();
            Assert.Equal(1,pos2.LineNumber);
            Assert.Equal(2, pos2.ColumnNumber);

            var pos3 = pos2.NextColumn();
            Assert.Equal(1,pos3.LineNumber);
            Assert.Equal(3, pos3.ColumnNumber);

            var pos4 = pos3.NextLine();
            Assert.Equal(2,pos4.LineNumber);
            Assert.Equal(1, pos4.ColumnNumber);
        }

        [Fact]
        public void BasicTextPositionOperatorTest()
        {
            var pos1 = TextPosition.Start;
            Assert.Equal(1,pos1.LineNumber);
            Assert.Equal(1, pos1.ColumnNumber);

            var pos2 = new TextPosition(1, 1);
            Assert.Equal(1,pos2.LineNumber);
            Assert.Equal(1, pos2.ColumnNumber);

            Assert.False(ReferenceEquals(pos1, pos2));
            Assert.True(pos1.Equals(pos2));
        }
    }
}
