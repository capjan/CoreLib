using System;
using System.Collections.Generic;
using System.Text;
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
            Assert.Equal(detail.IntegralPart, 2);
            Assert.Equal(detail.FractionPart, 0.73);
        }
    }
}
