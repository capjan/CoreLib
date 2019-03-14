using System.Collections.Generic;
using Core.Logging;
using Core.Logging.Targets;

namespace Core.Test.LoggingRelated
{
    internal class DumpLogTarget : LogTarget
    {
        /// <inheritdoc />
        protected override void OnLog(LogEventArgs itm)
        {
            EventLog.Add(itm);
        }

        public List<LogEventArgs> EventLog { get; } = new List<LogEventArgs>();
    }
}
