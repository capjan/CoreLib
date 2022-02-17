using System;
using System.Globalization;
using System.IO;
using System.Text;
using Core.Enums;
using Core.Localization.Impl;
using Core.Text.Formatter;

namespace Core.Extensions.TextRelated;

public static class TextFormatterExt
{
    public static string WriteToString<T>(this ITextFormatter<T> formatter, T value, string? newLine = default)
    {
        var sb = new StringBuilder();
        using (var stream = new StringWriter(sb))
        {
            stream.NewLine = newLine ?? stream.NewLine;
            formatter.Write(value, stream);
        }
        return sb.ToString();
    }

    public static string WriteToString(this IDateTimeFormatter formatter)
    {
        var sb = new StringBuilder();
        using (var stream = new StringWriter(sb))
            formatter.Write(stream);
        return sb.ToString();
    }

    public static void WriteLine<T>(this ITextFormatter<T> formatter, T value, TextWriter writer)
    {
        formatter.Write(value, writer);
        writer.WriteLine();
    }

    public static void Write(this IDateTimeFormatter formatter, TextWriter writer)
    {
        formatter.Write(DateTime.UtcNow, writer);
    }

    public static void WriteLine(this IDateTimeFormatter formatter, TextWriter writer)
    {
        formatter.Write(DateTime.UtcNow, writer);
        writer.WriteLine();
    }

    public static void MakeCultureInvariant<T>(this IFormattableTextFormatter<T> formatter)
    {
        formatter.FormatProvider = CultureInfo.InvariantCulture;
    }

    /// <summary>
    /// Convenient function to format a TimeSpan to something like "1 week, 2 hours"
    /// </summary>
    /// <param name="timeSpan">value to format</param>
    /// <param name="twoLetterLanguageCode">used localization for the formatted string</param>
    /// <param name="precision">precision of the formatted string</param>
    /// <param name="compact">use abbreviations for the units</param>
    /// <param name="separator">used separator between parts</param>
    /// <returns></returns>
    public static string ToFormattedString(
        this TimeSpan timeSpan,
        string twoLetterLanguageCode = "en",
        TimePart precision = TimePart.Minute,
        bool compact = false,
        string separator = ", ")
    {
        return new TimeSpanFormatter(
                localization: TimeLocalization.Create(twoLetterLanguageCode), 
                precision: precision, 
                compact: compact,
                separator: separator)
            .WriteToString(timeSpan);
    }

    public static string ToFormattedString<T>(this T value, ITextFormatter<T> formatter)
    {
        return formatter.WriteToString(value);
    }

    public static string ToFormattedString(this DateTime value, string? format = null, bool universalTime = false, IFormatProvider? formatProvider = default)
    {
        var formatter = new DateTimeFormatter(format: format, universalTime: universalTime, formatProvider: formatProvider);
        return value.ToFormattedString(formatter);
    }
}
