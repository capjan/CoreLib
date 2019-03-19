using System;
using System.Threading;
using Core.Logging.Targets;
using Core.Logging.Logger;

namespace SandBox
{
    class Program
    {
        private static bool Shutdown = false;
        public static void LogThreadA()
        {
            var logger = new ClassLogger<Program>();
            while (!Shutdown)
            {
                logger.Info("AAA AAA AAA AAA");
                Thread.Sleep(20);
            }
        }

        public static void LogThreadB()
        {
            var logger = new ClassLogger<Program>();
            while (!Shutdown)
            {
                logger.Info("BBB BBB BBB BBB");
                Thread.Sleep(20);
            }
        }

        public static void LogThreadC()
        {            
            while (!Shutdown)
            {
                if (!Console.KeyAvailable) continue;
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Z)
                    Shutdown = true;


            }
        }

        static void Main(string[] args)
        {
            var threadA = new Thread(LogThreadA);
            var threadB = new Thread(LogThreadB);
            //var threadC = new Thread(LogThreadC);

            using (var consoleTarget = new ColoredConsoleLogTarget
            {
                Connected         = true,
                DateTimeFormatter = {LocalTime = true}
            })
            {
                Console.WriteLine("Started Program (Press Z to exit)");
                threadA.Start();
                threadB.Start();
                // threadC.Start();

                var done = false;
                while (!done)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        {
                            if (key.Key == ConsoleKey.Z)
                            {
                                done = true;
                            }
                        }
                    }

                }
            }

            

            Shutdown = true;

            Console.WriteLine("Stopped Program. Press any key to exit");
            //var logger        = new ClassLogger<Program>();
            //var i = 0;
            //while (i < 50)
            //{
            //    logger.Trace($"Trace Message {i}");
            //    logger.Debug("Debug Message");
            //    logger.Info("Info Message");
            //    logger.Warning("Warning Message");
            //    logger.Error("Error Message");
            //    i++;
            //}
                        
            
            Console.ReadLine();
        }
    }
}
