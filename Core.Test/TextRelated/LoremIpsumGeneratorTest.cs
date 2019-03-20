using Core.Extensions.TextRelated;
using Core.Mathematics.Impl;
using Core.Text.Generator.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class LoremIpsumGeneratorTest
    {
        [Fact]
        public void BasicLoremIpsumGeneratorTest()
        {
            var random = new DefaultRandom(12345);
            var gen = new LoremIpsumGenerator(random);
            var text = gen.CreateText(1);
            Assert.Equal("lorem", text);
            text = gen.CreateText(10);
            Assert.Equal("lorem ipsum dolor sit amet stet kasd gubergren tempor ipsum", text);
        }
    }
}
