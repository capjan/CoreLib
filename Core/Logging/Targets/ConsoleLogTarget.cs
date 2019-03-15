using System;

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
            var createdAt = DateTimeFormatter.Format(itm.CreatedAtUtc);
            var level = itm.Level.ToString().PadLeft(LogLevelMaxCharLength);

            Console.WriteLine($"{createdAt} {level}: {itm.Message}");
        }        
    }
}
