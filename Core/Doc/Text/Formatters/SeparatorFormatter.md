# Separator Formatter

## Example
```csharp
var intArray = new[] {1, 2, 3, 4, 5};

var formatter = new DefaultSeparatorFormatter<int>(
    separator: ", ",
    groupLength: 1,
    toStringFunc: v => v.ToString(),
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
* `toStringFunc` - defines the callback function to create the string for the result string.
* `nullPlaceholder` - sets the text placeholder for null values.



