using System.Text;
using Core.Parser;

namespace Core.Extensions.ParserRelated;

public static class ParserInputExtensions
{
    /// <summary>
    /// Reads all remaining input chars as string.
    /// </summary>
    /// <param name="input">Input</param>
    /// <returns>All remaining chars of the input.</returns>
    public static string ReadAll(this IParserInput input)
    {
        var sb = new StringBuilder();
        while (input.TryReadChar(out var readChar))
        {
            sb.Append(readChar);
        }
        return sb.ToString();
    }
}