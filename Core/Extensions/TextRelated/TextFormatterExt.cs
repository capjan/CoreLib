using System.IO;
using System.Text;
using Core.Text.Formatter;

namespace Core.Extensions.TextRelated
{
    public static class TextFormatterExt
    {
        public static string FormatToString<T>(this ITextFormatter<T> formatter, T value)
        {
            var sb = new StringBuilder();
            using (var stream = new StringWriter(sb))
                formatter.WriteFormatted(value, stream);
            return sb.ToString();
        }
    }
}
