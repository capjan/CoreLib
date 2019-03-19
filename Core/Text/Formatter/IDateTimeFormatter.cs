using System;

namespace Core.Text.Formatter {
    public interface IDateTimeFormatter : ITextFormatter<DateTime>
    {
        string DateTimeFormat { get; set; }    
        bool   LocalTime      { get; set; }
    }
}