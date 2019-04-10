# Public IP Resolver

This interface makes it easy to resolve the public ip v4 address used by
the computer to access the internet.

## Interface
```csharp
public interface IPublicIpResolver
{
    string Resolve();
}
```

## How is the IP Address obtained?

The resolver tries to resolve the ip address via list aof given simple 
web services and returns the result of the first service with a valid
formatted result.

For convenience reasons the default resolver uses the following services 
if no list of services is provided.

1. https://ipinfo.io/ip
2. https://checkip.amazonaws.com/"
3. https://api.ipify.org
4. https://icanhazip.com
5. https://wtfismyip.com/text

## Return value

The resolver returns the public ip v4 address formatted as string. e.g. **123.456.789.0**
or throws an exception if the resolver failed.
Use the **TryResolve()** extension method if you want to work around the error handling.

## Examples

### Basic usage
```csharp
var ip = new DefaultPublicIpResolver().Resolve();
```

### via TryResolve()
```csharp
var resolver = new DefaultPublicIpResolver();
if (resolver.TryResolve(out var ip))
{
    // use the ip address
}
```

### Custom Web Services
```csharp
var serviceUrls = new [] {"https://customIpServcie.com"};
var resolver = new DefaultPublicIpResolver(serviceUrls: serviceUrls);
var ip = resolver.Resolve();
```
