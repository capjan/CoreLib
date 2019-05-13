using System.IO;
using Core.Extensions.TextRelated;
using Core.IO.Impl;
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

        [Fact]
        public void BasicLoremIpsumGeneratorToFileTest()
        {
            var random = new DefaultRandom(12345);
            var gen    = new LoremIpsumGenerator(random);

            var tmp = new DefaultTempUtil();
            tmp.UseFile(tempFile =>
            {
                using (var writer = new StreamWriter(tempFile))
                    gen.WriteText(10, writer);
                var fileContents = File.ReadAllText(tempFile);
                Assert.Equal("lorem ipsum dolor sit amet stet kasd gubergren tempor ipsum", fileContents);

            });                       
        }
    }
}
