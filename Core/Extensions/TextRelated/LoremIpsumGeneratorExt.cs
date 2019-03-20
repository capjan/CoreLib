
using System.IO;
using System.Text;
using Core.Text.Generator;

namespace Core.Extensions.TextRelated
{
    public static class LoremIpsumGeneratorExt
    {
        public static string CreateText(this ILoremIpsumGenerator generator, int wordCount)
        {
            using (var memoryStream = new MemoryStream())
            using (var textWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                generator.WriteText(wordCount, textWriter);
                textWriter.Flush();
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                    return streamReader.ReadToEnd();
            }
        }
    }
}
