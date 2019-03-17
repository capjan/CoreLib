using System.Diagnostics;

namespace Core.Logging.Targets
{
    public class DebugLogTarget : LogTarget
    {
        protected override void OnLog(LogEventArgs itm)
        {
            var createdAt = DateTimeFormatter.Format(itm.CreatedAtUtc);
            var level = LogLevelFormatter.Format(itm.Level);
            Debug.WriteLine($"{createdAt} {level}: {itm.Message}");
        }
    }
}
