using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Extensions.TextRelated;

public static class StringExt
{
    public static MemoryStream ToMemoryStream(this string value, bool writable = false, Encoding? encoding = default)
    {
        encoding = encoding ?? Encoding.UTF8;
        return new MemoryStream(encoding.GetBytes(value), writable);
    }

    public static StringReader ToStringReader(this string value)
    {
        return new StringReader(value);
    }

    /// <summary>
    /// Returns a string containing a specified number of characters from the right side of a string.
    /// </summary>
    /// <param name="value">input string</param>
    /// <param name="length">max chars to extract</param>
    /// <returns></returns>
    public static string Right(this string value, int length)
    {
        var maxLength = Math.Min(value.Length, length);
        var startOffset = value.Length - maxLength;
        return value.Substring(startOffset, maxLength);
    }

    /// <summary>
    /// Returns a string containing a specified number of characters from the left side of a string.
    /// </summary>
    /// <param name="value">input string</param>
    /// <param name="length">max chars to extract</param>
    /// <returns></returns>
    public static string Left(this string value, int length)
    {
        return value.Substring(0, Math.Min(value.Length, length));
    }

    /// <summary>
    /// Indicates whether the specified regular expression finds a match in the string
    /// </summary>
    /// <param name="value">input string</param>
    /// <param name="regexPattern">regular expression</param>
    /// <param name="options">options for the regex engine</param>
    /// <returns></returns>
    public static bool IsMatch(this string value, string regexPattern, RegexOptions options = RegexOptions.None)
    {
        return Regex.IsMatch(value, regexPattern, options);
    }
        

        
}