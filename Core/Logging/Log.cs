using System;
using System.Runtime.CompilerServices;
using Core.Enums;

namespace Core.Logging
{
    internal static class Log
    {
        public static object SyncLock = new object();

        public static event Action<LogEventArgs>? OnLog;

        internal static void Trace(
            string message,
            string callerClassFullName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            lock (SyncLock)
            {
                OnLog?.Invoke(new LogEventArgs(LogLevel.Trace, message, callerClassFullName, callerFilePath, callerMemberName, callerLineNumber));
            }
        }

        internal static void Debug(
            string message,
            string callerClassFullName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            lock (SyncLock)
            {
                OnLog?.Invoke(new LogEventArgs(LogLevel.Debug, message, callerClassFullName, callerFilePath, callerMemberName, callerLineNumber));
            }
        }

        internal static void Info(
            string message,
            string callerClassFullName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            lock (SyncLock)
            {
                OnLog?.Invoke(new LogEventArgs(LogLevel.Info, message, callerClassFullName, callerFilePath, callerMemberName, callerLineNumber));
            }

        }

        internal static void Warning(
            string message,
            string callerClassFullName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            lock (SyncLock)
            {
                OnLog?.Invoke(new LogEventArgs(LogLevel.Warning, message, callerClassFullName, callerFilePath, callerMemberName, callerLineNumber));
            }
        }

        internal static void Error(
            string message,
            Exception? exception = null,
            string callerClassFullName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            lock (SyncLock)
            {
                OnLog?.Invoke(new LogEventArgs(LogLevel.Error, message, callerClassFullName, callerFilePath, callerMemberName, callerLineNumber, exception));
            }
        }

    }
}
