using System;
using System.Runtime.CompilerServices;

// ReSharper disable ExplicitCallerInfoArgument
namespace Core.Logging.Logger
{
    internal class TypeLogger : ILogger 
    {
        public TypeLogger(string typeFullName)
        {
            _typeFullName = typeFullName;
        }

        public void Trace(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Trace(message, _typeFullName, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Debug(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Debug(message, _typeFullName, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Info(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Info(message, _typeFullName, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Warning(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Warning(message, _typeFullName, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Error(
            string message,
            Exception exception = null,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Error(message, exception, _typeFullName, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Error(
            Exception                 exception        = null,
            [CallerFilePath]   string callerFilePath   = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int    callerLineNumber = 0)
        {
            Log.Error("an exception occurred", exception, _typeFullName, callerFilePath, callerMemberName, callerLineNumber);
        }

        private readonly string _typeFullName;
    }
}
// ReSharper restore ExplicitCallerInfoArgument