using System;
using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class DefaultDateTimeFormatter : IDateTimeFormatter
    {
        public string DateTimeFormat { get; set; } = "dd.MM.yyyy HH:mm:ss.fff";
        public bool LocalTime { get; set; } = false;

        public void WriteFormatted(DateTime value, TextWriter writer)
        {
            if (LocalTime)
                value = value.ToLocalTime();

            writer.Write(value.ToString(DateTimeFormat));
        }
    }
}
