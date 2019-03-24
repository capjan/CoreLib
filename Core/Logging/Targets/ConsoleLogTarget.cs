using System;
using Core.Extensions.TextRelated;

namespace Core.Logging.Targets
{
    public class ConsoleLogTarget : LogTarget
    {
        public ConsoleLogTarget()
        {
            DateTimeFormatter.DateTimeFormat = "HH:mm:ss.ffff";
        }

        protected override void OnLog(LogEventArgs itm)
        {
            var createdAt = DateTimeFormatter.FormatToString(itm.CreatedAtUtc);
            var level = LogLevelFormatter.FormatToString(itm.Level);

            Console.WriteLine($"{createdAt} {level}: {itm.Message}");
        }        
    }
}
