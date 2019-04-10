using Core.Extensions.GenericsRelated;
using Xunit;

namespace Core.Test.GenericRelated
{
    public class GenericExtTest
    {
        [Fact]
        public void InTest()
        {
            Assert.True(6.In(12,3,4,5,6));
            Assert.False(5.In(1,2,3,4,6));
        }
    }
}
