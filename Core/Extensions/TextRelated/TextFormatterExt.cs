using System;
using System.IO;
using System.Text;
using Core.Text.Formatter;

namespace Core.Extensions.TextRelated
{
    public static class TextFormatterExt
    {
        public static string WriteToString<T>(this ITextFormatter<T> formatter, T value)
        {
            var sb = new StringBuilder();
            using (var stream = new StringWriter(sb))
                formatter.Write(value, stream);
            return sb.ToString();
        }

        public static string WriteToString(this IDateTimeFormatter formatter)
        {
            var sb = new StringBuilder();
            using (var stream = new StringWriter(sb))
                formatter.Write(stream);
            return sb.ToString();
        }

        public static void Write(this IDateTimeFormatter formatter, TextWriter writer)
        {
            formatter.Write(DateTime.UtcNow, writer);
        }
    }
}
