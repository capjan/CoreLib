using System.Collections.Generic;
using Core.Text.Formatter.Impl;

namespace Core.Extensions.TextRelated
{
    public static class ToHexStringExt
    {
        /// <summary>
        /// Creates a hexadecimal string representation of the data.
        /// </summary>
        /// <param name="data">data that should be converted to a string</param>
        /// <param name="upperCase">true if the ABCDEF hex chars should be written uppercase. false if lower case is preferred.</param>
        /// <returns></returns>
        public static string ToHexString(this IEnumerable<byte> data, bool upperCase = true)
        {
            var singleByteFormatter = new DefaultByteHexFormatter(upperCase);
            var formatter           = new DefaultByteListHexFormatter(singleByteFormatter);
            return formatter.FormatToString(data);
        }

        public static string ToHexString(this char value, bool upperCase = true)
        {
            return ((int) value).ToHexString(upperCase);
        }

        public static string ToHexString(this int value, bool upperCase = true)
        {
            var format = upperCase ? "{0:X}" : "{0:x}";
            return string.Format(format, value);
        }
    }
}
