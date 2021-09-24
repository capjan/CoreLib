# Separator Formatter

Think about the [SeparatorFormatter](../../../Text/Formatter/SeparatorFormatter.cs) as a [String.Join()](https://docs.microsoft.com/en-us/dotnet/api/system.string.join) on steroids, because it add Grouping, a ItemFormatter and a NullPlaceholder.

## Example
```csharp
var intArray = new[] {1, 2, 3, 4, 5};

var formatter = new SeparatorFormatter<int>(
    separator: ", ",
    groupLength: 1,
    itemFormatter: null,
    nullPlaceholder: "");

formatter.WriteFormatted(intArray, Console.Out);
// writes 1, 2, 3, 4, 5 to stdout

// same as above, but as string
var formattedString = formatter.WriteToString(intArray);

// via extension method-- 
var formattedViaExtension = intArray.ToSeparatedString();
```

With:
* `separator` - used separator. Defaults to `, `
* `groupLength` - sets how many items should be written until the separator is inserted. Defaults to 1.
* `itemFormatter` - defines the formatter function to create the string for the result string. Defaults to a LambdaFormatter that calls ToString()
* `nullPlaceholder` - sets the text placeholder for null values.



