using System.Collections.Generic;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;

namespace Core.Extensions.CollectionRelated
{
    public static class ByteCollectionExt
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
            var formatter = new DefaultByteListHexFormatter(singleByteFormatter);
            return formatter.FormatToString(data);
        }
    }
}
