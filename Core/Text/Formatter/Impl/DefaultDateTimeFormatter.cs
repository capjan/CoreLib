using System;
using System.IO;
using System.Threading;

namespace Core.Text.Formatter.Impl
{
    public class DefaultDateTimeFormatter : IDateTimeFormatter
    {
        public DefaultDateTimeFormatter(
            string format = "dd.MM.yyyy HH:mm:ss.fff", 
            bool localTime = true, 
            IFormatProvider formatProvider = default)
        {
            Format = format;
            LocalTime = localTime;
            FormatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
        }

        public void Write(DateTime value, TextWriter writer)
        {
            if (value.Kind != DateTimeKind.Utc)
                throw new ArgumentException($"{nameof(value)} is not of kind UTC");

            if (LocalTime)
                value = value.ToLocalTime();

            writer.Write(value.ToString(Format, FormatProvider));
        }
    
        public IFormatProvider FormatProvider { get; set; }
        public bool LocalTime { get; set; }
        public string Format { get; set; }
    }
}
