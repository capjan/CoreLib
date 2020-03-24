# Time Span Formatter

Formats TimeSpan like **2 weeks, 1 day, 1 hour, 5 minutes, 1 second** (Normal) or **1h, 5m, 1s** (Compact)

## Intended Usage

* Hide the concrete formatter implementation behind **ITextFormatter<TimeSpan>** to allow  Dependency Injection and changing the implementation without changing your code.
* CoreLib provides a default implementation called **DefaultTimeSpanFormatter**
* ITextFormatter
* Just use ToString() formatting for simple formatting purposes.

## Example

```c#
// or hide the implementation behind ITextFormatter<TimeSpan>
var formatter = new DefaultTimeSpanFormatter();

var value = new TimeSpan(1, 5, 4); // 1 hour, 5 Minutes, 4 Seconds

// write directly to Textwriter (Streamwriter, Console, etc.)
formatter.Write(value, Console.Out);
// writes "1 hour, 5 minutes, 4 seconds" to stdout

formatter.WriteLine(value, Console.Out);
// writes "1 hour, 5 minutes, 4 seconds\n" to stdout

// or write to a String
var str = formatter.WriteToString(value);
// str = "1 hour, 5 minutes, 4 seconds"


formatter.Compact = true;
Console.WriteLine(formatter.WriteToString(value));
// writes: 1h, 5m, 4s
```







