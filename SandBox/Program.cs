using System;
using Core.Logging.Targets;
using Core.Logging.Logger;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleTarget = new DebugLogTarget()
            {
                Connected = true, 
                DateTimeFormatter = {LocalTime = true}
            };
            var logger        = new ClassLogger<Program>();
            var i = 0;
            while (i < 50)
            {
                logger.Trace($"Trace Message {i}");
                logger.Debug("Debug Message");
                logger.Info("Info Message");
                logger.Warning("Warning Message");
                logger.Error("Error Message");
                i++;
            }
                        
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
