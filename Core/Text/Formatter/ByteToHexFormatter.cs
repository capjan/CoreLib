using System.IO;

namespace Core.Text.Formatter;

/// <summary>
/// Byte to Hex Formatter (Lookup Table based).
/// </summary>
public class ByteToHexFormatter : IByteHexFormatter
{
    public ByteToHexFormatter(bool upperCase = true)
    {
        var format = upperCase ? "X2" : "x2";
        _lookup = new string[byte.MaxValue+1];
        for (int index = byte.MinValue; index <= byte.MaxValue; index++)
        {
            _lookup[index] = index.ToString(format);
        }
    }
        
    public void Write(byte value, TextWriter writer)
    {
        writer.Write(_lookup[value]);
    }

    private readonly string[] _lookup;
}