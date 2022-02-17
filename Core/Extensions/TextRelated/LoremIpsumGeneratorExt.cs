using System.IO;
using System.Text;
using Core.Text.Generator;

namespace Core.Extensions.TextRelated;

public static class LoremIpsumGeneratorExt
{
    public static string CreateText(this ILoremIpsumGenerator generator, int wordCount)
    {
        var stringBuilder = new StringBuilder();
        using (var writer = new StringWriter(stringBuilder))
            generator.WriteText(wordCount, writer);
        return stringBuilder.ToString();
    }
}