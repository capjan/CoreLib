using System.Diagnostics;
using Core.Extensions.TextRelated;
// ReSharper disable All

namespace Core.Logging.Targets;

public class DebugLogTarget : LogTarget
{
    protected override void OnLog(LogEventArgs itm)
    {
        var createdAt = DateTimeFormatter.WriteToString(itm.CreatedAtUtc);
        var level = LogLevelFormatter.WriteToString(itm.Level);
        Debug.WriteLine($"{createdAt} {level}: {itm.Message}");
    }
}