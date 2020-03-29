# Time Span Formatter

Formats TimeSpan like **2 weeks, 1 day, 1 hour, 5 minutes, 1 second** (Normal) or **1h, 5m, 1s** (Compact)

## Intended Usage

* Features all benefits of [ITextFormatter](./ITextFormatter.md).
* CoreLib provides a default implementation called **DefaultTimeSpanFormatter**
* ITextFormatter
* Just use ToString() formatting for simple formatting purposes.

## Example

```c#
// or hide the implementation behind ITextFormatter<TimeSpan>
var formatter = new TimeSpanFormatter();

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

# Features

## Localization

It's easy to change the used localization. 

**Example:**

```C#
// creates a german localized TimeSpan formatter
var formatter = new DefaultTimeSpanFormatter(TimeLocalization.Create("de"));
var value = TimeSpan.FromHours(1.234);

formatter.WriteLine(value, Console.Out);
// writes "1 Tag, 5 Stunden, 36 Minuten, 57 Sekunden" to stdout
```

## Adjustable Precision

It's possible to set the precision of the formatter. The precision **defaults to Second**.

* If the precision of minutes is enough set it to Minute.
* if you measure runtime for performance optimizations set it to Millisecond.

**Example:**

```C#
var formatter = new DefaultTimeSpanFormatter(precision: TimePart.Minute);
var value = TimeSpan.FromHours(1.234);

formatter.WriteLine(value, Console.Out)
// writes "1 day, 5 hours, 36 minutes" to stdout
```

## Compact Mode

Activate the compact mode to use abbreviated units.

**Example:**

```C#
var formatter = new DefaultTimeSpanFormatter(compact: true);
var value = TimeSpan.FromHours(1.234);

formatter.WriteLine(value, Console.Out)
// writes "1d, 5h, 36m, 57s" to stdout
```

## Custom Separator

It's easy to replace the used separator (standard: comma) if necessary.

**Example:**

```C#
var formatter = new DefaultTimeSpanFormatter(separator: " - ");
var value = TimeSpan.FromHours(1.234);

formatter.WriteLine(value, Console.Out)
// writes "1 day - 5 hours - 36 minutes - 57 seconds" to stdout
```







