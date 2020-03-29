using System.Collections.Generic;
using Core.Text.Formatter;

namespace Core.Extensions.TextRelated
{
    public static class ToHexStringExt
    {
        /// <summary>
        /// Creates a hexadecimal string representation of the data.
        /// </summary>
        /// <param name="data">data that should be converted to a string</param>
        /// <param name="upperCase">true for uppercase hex chars. false for lowercase.</param>
        /// <returns></returns>
        public static string ToHexString(this IEnumerable<byte> data, bool upperCase = true)
        {
            var singleByteFormatter = new ByteToHexFormatter(upperCase);
            var formatter           = new EnumerableByteToHexFormatter(singleByteFormatter);
            return formatter.WriteToString(data);
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
