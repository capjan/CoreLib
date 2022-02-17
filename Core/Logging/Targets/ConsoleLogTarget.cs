using System;
using Core.Extensions.TextRelated;

namespace Core.Logging.Targets;

public class ConsoleLogTarget : LogTarget
{
    public ConsoleLogTarget()
    {
        DateTimeFormatter.Format = "HH:mm:ss.ffff";
    }

    protected override void OnLog(LogEventArgs itm)
    {
        var createdAt = DateTimeFormatter.WriteToString(itm.CreatedAtUtc);
        var level = LogLevelFormatter.WriteToString(itm.Level);

        Console.WriteLine($"{createdAt} {level}: {itm.Message}");
    }        
}