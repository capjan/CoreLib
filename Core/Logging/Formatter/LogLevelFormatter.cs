using Core.Text.Formatter;

namespace Core.Logging.Formatter
{
    public class LogLevelFormatter : ITextFormatter<LogLevel>
    {
        public LogLevelFormatter()
        {
            _logLevelMaxCharLength = nameof(LogLevel.Warning).Length; 
        }
        public string Format(LogLevel value)
        {
            return value.ToString().PadLeft(_logLevelMaxCharLength);
        }

        private readonly int _logLevelMaxCharLength;
    }
}
