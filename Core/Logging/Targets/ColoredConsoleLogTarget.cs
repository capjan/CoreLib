using System;
using Core.Extensions.TextRelated;

namespace Core.Logging.Targets
{
    public class ColoredConsoleLogTarget : LogTarget
    {
        public ColoredConsoleLogTarget()
        {
            DateTimeFormatter.DateTimeFormat = "HH:mm:ss.ffff";
        }

        protected override void OnLog(LogEventArgs itm)
        {
            var createdAt = DateTimeFormatter.FormatToString(itm.CreatedAtUtc);
            var level = LogLevelFormatter.FormatToString(itm.Level);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(createdAt);
            Console.Write(' ');
            var levelColor = GetLevelColor(itm.Level);

            Console.ForegroundColor = levelColor;
            Console.Write(level);
            Console.Write(": ");
            Console.ResetColor();
            Console.WriteLine(itm.Message);                        
        }

        private static ConsoleColor GetLevelColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace: return ConsoleColor.DarkGray;
                case LogLevel.Debug: return ConsoleColor.Gray;
                case LogLevel.Info: return ConsoleColor.Cyan;
                case LogLevel.Warning: return ConsoleColor.Yellow;
                case LogLevel.Error: return ConsoleColor.Red;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
