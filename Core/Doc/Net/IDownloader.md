# IDownloader

This interface makes the inplementation to download a file exchangeable.

## Interface
```csharp
public interface IDownloader
{
    string DownloadToString(string url);

    // via extension methods
    bool   TryDownloadToString(string url, out string result, string fallback = default)
}
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
  `DownloadToString()` throws an exception, `TryDownloadToString()` returns `false` and `result` is the fallback.
  The content of an error page is never returned as a result.
* `DefaultDownloader` uses one `HttpClient` for all downloads of the application. Pass your own client to
  configure it (proxy, timeout, ...). It is not disposed by the downloader:

  ```csharp
  var downloader = new DefaultDownloader(myHttpClient);
  ```

* `DownloaderWithCredentials` creates its client with the first download and reuses it afterwards.
* `HttpChannelDownloader` decodes the text with the charset the server announces (UTF-8 if there is none).
  Pass an `Encoding` to the constructor to force a specific encoding. A byte order mark in the content takes precedence.

