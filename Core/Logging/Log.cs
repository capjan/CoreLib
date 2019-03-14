using System;
using System.Runtime.CompilerServices;

namespace Core.Logging
{
    public static class Log
    {
        public static event Action<LogEventArgs> OnLog;

        internal static void Trace(   
            string message,
            string callerClassName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            OnLog?.Invoke(new LogEventArgs(LogLevel.Trace, message, callerClassName, callerFilePath, callerMemberName, callerLineNumber));
        }

        internal static void Debug(
            string message,
            string callerClassName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            OnLog?.Invoke(new LogEventArgs(LogLevel.Debug, message, callerClassName, callerFilePath, callerMemberName, callerLineNumber));
        }

        internal static void Info(
            string message,
            string callerClassName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            OnLog?.Invoke(new LogEventArgs(LogLevel.Info, message, callerClassName, callerFilePath, callerMemberName, callerLineNumber));
        }

        internal static void Warning(
            string message,
            string callerClassName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            OnLog?.Invoke(new LogEventArgs(LogLevel.Warning, message, callerClassName, callerFilePath, callerMemberName, callerLineNumber));
        }

        internal static void Error(
            string message,
            Exception exception = null,
            string callerClassName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            OnLog?.Invoke(new LogEventArgs(LogLevel.Error, message, callerClassName, callerFilePath, callerMemberName, callerLineNumber, exception));
        }

    }
}
