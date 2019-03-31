using System.Text;
using Core.Extensions.TextRelated;
using Core.Security.Cryptography.Security;
using Xunit;

namespace Core.Test.SecurityRelated
{
    public class CRC32Test
    {
        [Fact]
        public void BasicTest()
        {
            var data = Encoding.UTF8.GetBytes("Hello World");
            var crc32 = new Crc32HashProvider();
            var result = crc32.ComputeHash(data).ToHexString();
            Assert.Equal("4A17B156", result);
        }
    }
}
