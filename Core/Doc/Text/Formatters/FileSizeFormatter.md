# FileSizeFormatter

## Features

- Automatic SiPrefix Resolving
- Possibility to force a given SiPrefix (e.g. format all in Mebibyte)
- Culture dependent number formatting via FormatProvider property (See: [IFormatProvider](https://docs.microsoft.com/en-us/dotnet/api/system.iformatprovider?view=net-6.0) at Microsoft Docs)
- Custom formatting strings via Format property (See: [standard](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) and [custom](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-numeric-format-strings) numeric format at Microsoft Docs)

## Example
```csharp
var formatter = new FileSizeFormatter
{
    Format = "0.##",
    FormatProvider = CultureInfo.InvariantCulture
};

var fileSize = 123456789L;

// formatter chooses by default the most readable format (here MiB)        
formatter.Write(fileSize, Console.Out); // 117.74 MiB 

// formatter allows to fore the SiPrefix (here we fore to Kibibyte)
formatter.ForcedUnit = BinaryUnitPrefix.Kibi;
formatter.Write(fileSize, Console.Out); // 120563.27 KiB

// formatter allows to fore to byte (raw size) by setting the forced unit to None
formatter.ForcedUnit = BinaryUnitPrefix.None;
formatter.Write(fileSize, Console.Out); // 123456789 B
```