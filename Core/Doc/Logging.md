# Logging

Logging should be easy to use, filterable and shouldn't add external dependencies.

# Architecture
 * the Log is implemented as static class
 * the log exposes an action event **OnLog** when a log message is written
 * Log targets can attach or detach to this event at every time
 * every class shound use a class logger to log messages, because this messages are filterable by namespace

# How to Log

```csharp
public class Program
{
    static void Main(string[] args)
    {
        // Add log targets. attach as many as required
        using (var consoleLogger = new ColoredConsoleLogTarget())
        {
            // create your logger
            var log = new ClassLogger<Program>();

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
public class YourClass
{
    private ILogger _log = new ClassLogger<YourClass>();

    public YourClass()
    {
        _log.Trace($"Instance of {nameof(YourClass)} created");
    }
}
```