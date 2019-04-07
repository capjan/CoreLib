using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using Core.Enums;
using Core.Security.Cryptography.Security;

namespace Core.Extensions.TextRelated
{
    public static class StringExt
    {
        public static MemoryStream ToMemoryStream(this string value, bool writable = false, Encoding encoding = default)
        {
            encoding = encoding ?? Encoding.UTF8;
            return new MemoryStream(encoding.GetBytes(value), writable);
        }

        public static StringReader ToStringReader(this string value)
        {
            return new StringReader(value);
        }

        // <summary>
        /// Returns a string containing a specified number of characters from the right side of a string.
        /// </summary>
        public static string Right(this string value, int length)
        {
            var maxLength = Math.Min(value.Length, length);
            var startOffset = value.Length - maxLength;
            return value.Substring(startOffset, maxLength);
        }

        // <summary>
        /// Returns a string containing a specified number of characters from the left side of a string.
        /// </summary>
        public static string Left(this string value, int length)
        {
            return value.Substring(0, Math.Min(value.Length, length));
        }
        

        
    }
}
