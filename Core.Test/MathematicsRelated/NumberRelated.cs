using Core.Extensions.MathematicsRelated;
using Xunit;

namespace Core.Test.MathematicsRelated
{
    public class NumberRelated
    {
        [Fact]
        public void DoubleTest()
        {
            var value = 2.73;
            var detail = value.Details();
            Assert.Equal(2, detail.IntegralPart);
            Assert.Equal(0.73, detail.FractionPart);
        }
    }
}
