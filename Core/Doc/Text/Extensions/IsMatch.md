# IsMatch (string extension)

IsMatch is a convenience shortcut for **RegEx.IsMatch(input, pattern, ...)**

## Example

```csharp
var ip = "127.0.0.1";
var pattern = @"\d{0,3}(\.\d{0,3}){3}";

if (!ip.IsMatch(pattern))
{
    Console.WriteLine("Alert! The ip address is invalid.")
}
```