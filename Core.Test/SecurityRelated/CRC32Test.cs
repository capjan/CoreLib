using System.Text;
using Core.Extensions.SecurityRelated;
using Core.Extensions.TextRelated;
using Core.Security.Cryptography.Security;
using Xunit;

namespace Core.Test.SecurityRelated
{
    public class Crc32Test
    {
        [Fact]
        public void BasicTest()
        {
            var data   = Encoding.UTF8.GetBytes("Hello World");
            var crc32  = new CRC32HashProvider();
            var result = crc32.ComputeHash(data).ToHexString();
            Assert.Equal("4A17B156", result);
        }

        [Fact]
        public void TestStringExtensions()
        {
            Assert.Equal("4A17B156", "Hello World".CalcCRC32());
            Assert.Equal("00000000", "".CalcCRC32());
            Assert.Equal("54A0C7BD", "Jan".CalcCRC32());
        }
    }
}