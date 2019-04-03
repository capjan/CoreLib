using System;

namespace Core.Text.Formatter
{
    public interface IDateTimeFormatter : IFormattableTextFormatter<DateTime>
    {
        bool LocalTime { get; set; }
    }
}