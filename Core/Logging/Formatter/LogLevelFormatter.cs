using System.IO;
using Core.Enums;
using Core.Text.Formatter;

namespace Core.Logging.Formatter;

public class LogLevelFormatter : ITextFormatter<LogLevel>
{
    public LogLevelFormatter()
    {
        _logLevelMaxCharLength = nameof(LogLevel.Warning).Length; 
    }

    public void Write(LogLevel value, TextWriter writer)
    {
        writer.Write(value.ToString().PadLeft(_logLevelMaxCharLength));            
    }

    private readonly int _logLevelMaxCharLength;
}