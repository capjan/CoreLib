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

        public void WriteFormatted(IEnumerable<byte> value, TextWriter writer)
        {
            foreach (var b in value)
            {
                _singleByteFormatter.WriteFormatted(b, writer);
            }            
        }

        private readonly IByteHexFormatter _singleByteFormatter;        
    }
}
