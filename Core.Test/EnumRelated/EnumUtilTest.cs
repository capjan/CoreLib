using System.Linq;
using Core.Enums;
using Xunit;

namespace Core.Test.EnumRelated
{
    public class EnumUtilTest
    {
        [Fact]
        public void BasicTest()
        {
            var info = EnumUtil.List<System.Environment.SpecialFolder>().OrderBy(i=>i.Name).ToArray();
            Assert.Equal("AdminTools", info[0].Name);
            
        }
    }
}
