using System.Collections.Generic;
using System.IO;

namespace Core.Text.Formatter
{
    /// <summary>
    /// Enumerable Byte to Hex Formatter.
    /// </summary>
    public class EnumerableByteToHexFormatter : IByteListHexFormatter
    {
        public EnumerableByteToHexFormatter(
            IByteHexFormatter singleByteFormatter = default)
        {
            _singleByteFormatter = singleByteFormatter ?? new ByteToHexFormatter();
        }

        /// <summary>
        /// Instantiates a formatter that formats the bytes in the given casing. 
        /// </summary>
        /// <param name="uppercase">true for uppercase output - false for lowercase output</param>
        public EnumerableByteToHexFormatter(bool uppercase) : this(new ByteToHexFormatter(uppercase)) {}

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
