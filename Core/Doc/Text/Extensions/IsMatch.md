# IsMatch (string extension)

IsMatch is a convenience shortcut for **RegEx.IsMatch(input, pattern, ...)**

## Example

```csharp
var ip = "127.0.0.1";

// pattern taken from: https://stackoverflow.com/questions/5284147/validating-ipv4-addresses-with-regexp/36760050#36760050
var pattern = @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$";

if (!ip.IsMatch(pattern))
{
    Console.WriteLine("Alert! The ip address is invalid.")
}
```
