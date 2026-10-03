# Public IP Resolver

These interfaces resolve the public IPv4 address the computer uses to access the internet.

## Interface
```csharp
using System.Threading;
using System.Threading.Tasks;

public interface IPublicIpResolver
{
    string Resolve();
}

public interface IAsyncPublicIpResolver
{
    Task<string> ResolveAsync(CancellationToken cancellationToken = default);
}
```

`DefaultPublicIpResolver` implements both interfaces. Prefer `ResolveAsync()` in asynchronous applications so the HTTP request does not block a thread. Pass a cancellation token to cancel the request. If you provide a custom synchronous `IDownloader`, also provide an `IAsyncDownloader` as the `asyncDownloader` argument to use `ResolveAsync()`.

## How is the IP Address obtained?

The resolver queries its configured services in order and returns the first response that contains a valid IPv4 address.

The default resolver uses these services when no list of service URLs is provided:

1. https://ipinfo.io/ip
2. https://checkip.amazonaws.com/
3. https://api.ipify.org
4. https://icanhazip.com
5. https://wtfismyip.com/text

## Return value

The resolver returns the public IPv4 address as a string (for example, **123.45.67.89**) or throws an exception if resolution fails.
Use the **TryResolve()** extension method if you want to work around the error handling.

## Examples

### Basic usage
```csharp
var ip = new DefaultPublicIpResolver().Resolve();
```

### Asynchronous usage
```csharp
using System.Threading;

var resolver = new DefaultPublicIpResolver();
var ip = await resolver.ResolveAsync(CancellationToken.None);
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
var serviceUrls = new [] {"https://customIpService.com"};
var resolver = new DefaultPublicIpResolver(serviceUrls: serviceUrls);
var ip = resolver.Resolve();
```
