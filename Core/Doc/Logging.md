# Logging

[Home](./README.md)

Logging should ...

* be easy to use
* be easy to extend
* be filterable (conditional logging)
* add no external dependencies (expect itself)

# Architecture
 * Core of this logging implementatin is the static internal class *Log*
 * *Log* exposes static methods for logging and a global event to notify targets
 * Targets are implemented by inheriting from the abstract class LogTarget. 

# How to Log

```csharp
public class Program
{
    static void Main(string[] args)
    {
        // add targets. attach as many as required
        using (var consoleLogger = new ColoredConsoleLogTarget())
        {
            // create your logger
            var log = Logger.Create<Program>();

            // start logging
            log.Info("This is my first log message");
        }
    }
}
```

# Targets

Log target can be found at namespace *Core.Logging.Targets*

 * Debug (Visual Studio Output Window)
 * Colored Console
 * Console
 * File
 * Custom Log Targets by creating a class inherited from LogTarget

# Best Practices
Spend for every class its own logger
```csharp
public class ExampleClass
{
    private ILogger _log = Logger.Create<ExampleClass>();

    public YourClass()
    {
        _log.Trace($"Instance of {nameof(ExampleClass)} created");
    }
}
```

# NLog, log4Net, ...

As already mentioned, this core logging approach is easy to extend. 
Following the SOLID Open Closed principe CoreLib is open for extensions, 
but not for modifications.

So, if we require any feature from the popular (and great) logging libraries 
out there you can add them **without changing our existing code**! 

All you have to do is to implement you custom LogTarget and forward your 
logging messages to the chosen logging library.