# Formatters

All Formatters shared this common interface

```csharp
public interface ITextFormatter<in T>
{
    string Format(T value);
}
```

## IDateTimeFormatter
Formats DateTime objects to a string representation. 

```csharp
public interface IDateTimeFormatter : ITextFormatter<DateTime>
{
    string DateTimeFormat { get; set; }    
    bool   LocalTime      { get; set; }
}
```
With:
* `DateTimeFormat` - Format String of the dateTime. Defaults to "dd.MM.yyyy HH:mm:ss.fff"
* `LocalTime` - value should be formatted in local time. value is assumed to be always UTC

