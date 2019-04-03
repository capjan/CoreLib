using System.Collections.Generic;
using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class DefaultByteListHexFormatter : IByteListHexFormatter
    {
        public DefaultByteListHexFormatter(
            IByteHexFormatter singleByteFormatter = default)
        {
            _singleByteFormatter = singleByteFormatter ?? new DefaultByteHexFormatter();
        }

        public void Write(IEnumerable<byte> value, TextWriter writer)
        {
            foreach (var b in value)
            {
                _singleByteFormatter.Write(b, writer);
            }
        }

        private readonly IByteHexFormatter _singleByteFormatter;
    }
}
