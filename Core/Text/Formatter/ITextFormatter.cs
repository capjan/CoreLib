using System;

namespace Core.Text.Formatter
{
    public interface ITextFormatter<in T>
    {
        string Format(T value);
    }

    public interface IDateTimeFormatter : ITextFormatter<DateTime>
    {
        string DateTimeFormat { get; set; }        
        bool   LocalTime      { get; set; }
    }
}
