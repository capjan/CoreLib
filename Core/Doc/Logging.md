# Logging

Logging should ...

* be easy to use
* be easy to extend
* be thread safe
* be filterable (conditional logging)
* add no external dependencies - expect itself for sure :)

# Other Libraries

I prefer to keep it simple stupid, but if you want to use one of the other great logging solutions
like NLog, log4net, ASP.NET Log due to it's feature set or simply if you are forced to - you can!

Simply add a custom log target to feed the 3rd party library and you're done. You don't have to rewrite or change your code!
Be open for extensions, but not for modifications.


# Architecture
 * the core of logging is implemented in a single static class *Log*
 * this static log class exposes for every log entry an action event *OnLog*
 * the action exposes all required information to implement a log targets 
 * targets can attach or detach to this event at every time
 

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