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
            DateTimeFormat = format;
            _localTime = localTime;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
        }

        public void WriteFormatted(DateTime value, TextWriter writer)
        {
            if (value.Kind != DateTimeKind.Utc)
                throw new ArgumentException($"{nameof(value)} is not of kind UTC");

            if (_localTime)
                value = value.ToLocalTime();            

            writer.Write(value.ToString(DateTimeFormat, _formatProvider));
        }
    
        private readonly IFormatProvider _formatProvider;
        private readonly bool _localTime;
        public string DateTimeFormat { get; set; }
    }
}
