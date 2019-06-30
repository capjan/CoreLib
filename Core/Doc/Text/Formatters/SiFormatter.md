# SI Formatter

Formats a given number (double) to a number with an unit and a metric SI prefix to indicate multiples or factions
of the unit.

e.g. 1000 => 1 k

## Features
* direct writing to a TextWriter with convenient extension methods to write to a string
* Features Format and IFormatProvider to format the value
* auto determination of the SI prefix. Forcing to a given SI Prefix is also possible
* possibility to set the name of the unit. defaults to null (no unit)
* possibility to set the delimiter between value and unit. e.g. replace a space by a tab etc.
 
## Examples
```csharp
var formatter = new SiFormatter();
formatter.MakeCultureInvariant(); // extension to make the formatter culture invariant

var result = formatter.FormatToString(1000);
// result contains 1 k
```

Force Mega SI-Prefix
```csharp
var formatter = new SiFormatter();
formatter.MakeCultureInvariant(); // extension to make the formatter culture invariant
formatter.ForceMega();

var result = formatter.WriteToString(144e4);
// result contains 1.44 M
```

Force Mega SI-Prefix and unit Byte (B)
```csharp
var formatter = new SiFormatter();
formatter.MakeCultureInvariant();
formatter.ForceMega();
formatter.Unit = "B"; // Byte

var result = formatter.WriteToString(144e4);
// result contains 1.44 MB
```



