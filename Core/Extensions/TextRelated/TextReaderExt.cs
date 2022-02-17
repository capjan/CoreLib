using System.IO;

namespace Core.Extensions.TextRelated;

public static class TextReaderExt
{
    public static bool TryPeekChar(this TextReader textReader, out char ch)
    {
        var peekedChar = textReader.Peek();
        if (peekedChar == -1)
        {
            ch = default;
            return false;
        }
        ch = (char) peekedChar;
        return true;
    }

    public static bool TryReadChar(this TextReader textReader, out char ch)
    {
        var readChar = textReader.Read();
        if (readChar == -1)
        {
            ch = default;
            return false;
        }
        ch = (char) readChar;
        return true;
    }
}