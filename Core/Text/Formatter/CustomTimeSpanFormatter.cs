using System;
using System.IO;
using System.Globalization;

namespace Core.Text.Formatter;

public class CustomTimeSpanFormatter : ITextFormatter<TimeSpan>
{
    private readonly string _format;

    public CustomTimeSpanFormatter(string format)
    {
        _format = format;
    }

    public void Write(TimeSpan value, TextWriter writer)
    {
        var formattedValue = value.ToString(_format, CultureInfo.CurrentCulture);
        writer.Write(formattedValue);
    }
}