# Time Span Formatter

Formats TimeSpan like **2 weeks, 1 day, 1 hour, 5 minutes, 1 second** (Normal) or **1h, 5m, 1s** (Compact)

## Intended Usage

* Hide the concrete formatter implementation behind **ITextFormatter<TimeSpan>** when using Dependency Injection.
* CoreLib provides a Default implementation **DefaultTimeSpanFormatter**
* Just use ToString() formatting for simple formatting purposes.

## Examples

```c#
// or hide the implementation behind ITextFormatter<TimeSpan>
var formatter = new DefaultTimeSpanFormatter();

var value = new TimeSpan(1, 5, 4); // 1 hour, 5 Minutes, 4 Seconds

Console.WriteLine(formatter.WriteToString(value));
// writes: 1 hour, 5 minutes, 4 seconds

formatter.Compact = true;
Console.WriteLine(formatter.WriteToString(value));
// writes: 1h, 5m, 4s
```







