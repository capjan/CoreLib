using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Logging.Targets
{
    public class DebugLogTarget : LogTarget
    {
        protected override void OnLog(LogEventArgs itm)
        {
            var createdAt = DateTimeFormatter.Format(itm.CreatedAtUtc);
            var level =  itm.Level.ToString().PadLeft(LogLevelMaxCharLength);
            Debug.WriteLine($"{createdAt} {level}: {itm.Message}");
        }
    }
}
