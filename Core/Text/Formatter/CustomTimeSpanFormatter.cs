using System;
using System.IO;

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
        var formattedValue = value.ToString(_format);
        writer.Write(formattedValue);
    }
}