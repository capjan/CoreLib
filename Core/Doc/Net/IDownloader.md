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

