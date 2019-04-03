using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class DefaultByteHexFormatter : IByteHexFormatter
    {
        public DefaultByteHexFormatter(bool upperCase = true)
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
}
