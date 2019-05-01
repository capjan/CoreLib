# IUrlEncoder

.NET provides several implementations to encode an url (see: [Stackoverflow: URL Encoding using C#](https://stackoverflow.com/questions/575440/url-encoding-using-c-sharp))
 
To make the existing source code future-proof, I decided to hide the implementation behind an interface.

## Notes

The default implementation currently uses [WebUtility.UrlEncode](https://docs.microsoft.com/de-de/dotnet/api/system.net.webutility.urlencode)
because it doesn't require an addtitonal reference to System.Web like [HttpUtility.EncodeUrl()](https://docs.microsoft.com/de-de/dotnet/api/system.web.httputility.urlencode).

## Interface
```csharp
public interface IUrlEncoder
{
    string Encode(string value);
}
```

## Examples

### Basic usage
```csharp
var encoder = new DefaultUrlEncoder();
var value = encoder.Encode("Hello World");
// value = "Hello+World"
```

