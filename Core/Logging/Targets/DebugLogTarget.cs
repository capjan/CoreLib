using System.Diagnostics;
using Core.Extensions.TextRelated;

namespace Core.Logging.Targets
{
    public class DebugLogTarget : LogTarget
    {
        protected override void OnLog(LogEventArgs itm)
        {
            var createdAt = DateTimeFormatter.FormatToString(itm.CreatedAtUtc);
            var level = LogLevelFormatter.FormatToString(itm.Level);
            Debug.WriteLine($"{createdAt} {level}: {itm.Message}");
        }
    }
}
