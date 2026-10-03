# IDownloader

This interface makes the implementation to download a file exchangeable.

## Interface
```csharp
using System.Threading;
using System.Threading.Tasks;

public interface IDownloader
{
    string DownloadToString(string url);
}

public interface IAsyncDownloader
{
    Task<string> DownloadToStringAsync(string url, CancellationToken cancellationToken = default);
}

public static class DownloaderExt
{
    public static bool TryDownloadToString(this IDownloader downloader, string url, out string result, string fallback = "");
}
```

`TryDownloadToString` is an extension method, not an interface member.

Prefer the asynchronous API in new code. It accepts a cancellation token and avoids blocking a thread while the HTTP request is in progress. The built-in downloader implementations implement both interfaces during the migration period.

```csharp
using System.Threading;
using Core.Net.Impl;

var downloader = new DefaultDownloader();
var result = await downloader.DownloadToStringAsync("https://www.example.com", CancellationToken.None);
```

## Examples

```csharp
var downloader = new DefaultDownloader();

if (!downloader.TryDownloadToString("https://www.example.com", out var result))
{
    // show an error message and quit.
    return;
}

// success: result now contains the downloaded web resource.
```

## Behavior

* A download **fails** if the server answers with an error status code (e.g. 404 or 500) or the server can't be reached.
  `DownloadToString()` throws the exception itself (an `HttpRequestException`, not wrapped in an `AggregateException`),
  `TryDownloadToString()` returns `false` and `result` is the fallback.
  The content of an error page is never returned as a result.
* `DefaultDownloader` uses one `HttpClient` for all downloads of the application. That client does **not** store cookies,
  so downloads of different users or jobs never see each other's session. Pass your own client to configure it
  (proxy, timeout, cookies, ...). It is not disposed by the downloader:

  ```csharp
  var downloader = new DefaultDownloader(myHttpClient);
  ```

* `DownloaderWithCredentials` creates its client with the first download and reuses it afterwards. It does not store cookies either.
* `HttpChannelExt.SharedHttpClient`, which the `IHttpChannel` extension methods use, is the same kind of client and does not store cookies either. New code can use `DownloadToStringAsync` and `DownloadHeaderAsync` with cancellation.
  Assign your own client to it if you need a session.
* `HttpChannelDownloader` decodes the text with the charset the server announces (UTF-8 if there is none).
  Pass an `Encoding` to the constructor to force a specific encoding. A byte order mark in the content still wins over
  that encoding. If the server announces a charset, a byte order mark is not looked at (as in `HttpContent.ReadAsStringAsync`).
* On `netstandard2.0` (e.g. .NET Framework) the shared clients cannot recycle their connections: a long-lived client keeps using
  the address it resolved first until the connection breaks. Hosts that need to follow DNS changes there should pass their own client.
