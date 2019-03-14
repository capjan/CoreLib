using System;

namespace Core.Logging
{
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// very detailed logs, which may include high-volume information such as protocol payloads. This log level is typically only enabled during development
        /// </summary>
        Trace = 1,

        /// <summary>
        /// debugging information, less detailed than trace, typically not enabled in production environment.
        /// </summary>
        Debug = 2,

        /// <summary>
        /// information messages, which are normally enabled in production environment
        /// </summary>
        Info = 4,

        /// <summary>
        /// warning messages, typically for non-critical issues, which can be recovered or which are temporary failures.
        /// </summary>
        Warning = 8,

        /// <summary>
        /// error messages - most of the time these are Exceptions
        /// </summary>
        Error = 16,

        AllMask = Trace | Debug | Info | Warning | Error,
        ProductionMask = Info | Warning | Error
    }
}
