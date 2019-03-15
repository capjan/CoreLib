using System;
using System.Runtime.CompilerServices;

// ReSharper disable ExplicitCallerInfoArgument
namespace Core.Logging.Logger
{
    public class ClassLogger<T> : ILogger where T : class
    {
        public ClassLogger()
        {
            var type = typeof(T);
            _className = type.Name;
        }

        public void Trace(   
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Trace(message, _className, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Debug(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Debug(message, _className, callerFilePath, callerMemberName, callerLineNumber);   
        }

        public void Info(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Info(message, _className, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Warning(
            string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Warning(message, _className, callerFilePath, callerMemberName, callerLineNumber);
        }

        public void Error(
            string message,
            Exception exception = null,
            [CallerFilePath] string callerFilePath = "",
            [CallerMemberName] string callerMemberName = "",
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Log.Error(message, exception, _className, callerFilePath, callerMemberName, callerLineNumber);
        }
        private readonly string _className;
    }
}
// ReSharper restore ExplicitCallerInfoArgument