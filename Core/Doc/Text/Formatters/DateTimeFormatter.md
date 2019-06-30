# Date Time Formatter

You should start using UTC only DateTimes as early as possible. 

## Intended usage
* Store all DateTime values in UTC!
* Use `DateTime.UtcNow` instead of `DateTime.Now` to get the current DateTime.
* Use a IDateTimeFormatter with `LocalTime = true` to show local DateTime strings to the local users.

## Example
```csharp
IDateTimeFormatter formatter = new DefaultDateTimeFormatter(
                format: " dd.MM.yyyy HH:mm:ss.fff ",
                localTime: true,
                formatProvider: CultureInfo.CurrentCulture);
var localTime = formatter.WriteToString(DateTime.UtcNow);
```

With:
* `format` - format string of the dateTime. Defaults to `dd.MM.yyyy HH:mm:ss.fff`
* `localTime` - true, if the value should be formatted in local time. Defaults to `true`.
* `formatProvider` - used format provider. Defaults to `CultureInfo.CurrentCulture`



