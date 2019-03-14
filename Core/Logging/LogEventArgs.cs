using System;

namespace Core.Logging
{
    public class LogEventArgs
    {
        public LogEventArgs(LogLevel level, string message, string callerClassName, string callerFilePath, string callerMemberName, int callerLineNumber, Exception exception = null)
        {
            Level = level;
            Message = message;
            CallerClassName = callerClassName;
            CallerFilePath = callerFilePath;
            CallerMemberName = callerMemberName;
            CallerLineNumber = callerLineNumber;
            CreatedAtUtc = DateTime.UtcNow;
            Exception = exception;
        }

        public DateTime CreatedAtUtc { get; }
        public LogLevel Level { get; }
        public string Message { get; }
        public string CallerClassName { get; }
        public string CallerFilePath { get; }
        public string CallerMemberName { get; }
        public int CallerLineNumber { get; }
        public Exception Exception { get; }

    }
}
