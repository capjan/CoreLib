using System;
using System.IO;
using System.Threading;

namespace Core.Text.Formatter.Impl
{
    public class DefaultDateTimeFormatter : IDateTimeFormatter
    {
        public DefaultDateTimeFormatter(
            string format = "dd.MM.yyyy HH:mm:ss.fff", 
            bool useLocalTime = false, 
            IFormatProvider formatProvider = default)
        {
            DateTimeFormat = format;
            _useLocalTime = useLocalTime;
            _formatProvider = formatProvider ?? Thread.CurrentThread.CurrentCulture;
        }

        public void WriteFormatted(DateTime value, TextWriter writer)
        {
            if (_useLocalTime)
                value = value.ToLocalTime();

            writer.Write(value.ToString(DateTimeFormat, _formatProvider));
        }
    
        private readonly IFormatProvider _formatProvider;
        private readonly bool _useLocalTime;
        public string DateTimeFormat { get; set; }
    }
}
