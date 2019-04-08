using System.Text;
using Core.Extensions.SecurityRelated;
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

        [Fact]
        public void TestStringExtensions()
        {
            Assert.Equal("4A17B156", "Hello World".CalcCrc32());
            Assert.Equal("00000000", "".CalcCrc32());
            Assert.Equal("54A0C7BD", "Jan".CalcCrc32());
        }
    }
}
