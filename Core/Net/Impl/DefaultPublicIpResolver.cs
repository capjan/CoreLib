using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions.NetRelated;
using Core.Extensions.TextRelated;
using Core.Text;

namespace Core.Net.Impl;

public class DefaultPublicIpResolver : IPublicIpResolver, IAsyncPublicIpResolver
{
    public DefaultPublicIpResolver(
        IDownloader? downloader = default,
        string[]? serviceUrls = default,
        IAsyncDownloader? asyncDownloader = default)
    {
        _downloader = downloader ?? new DefaultDownloader();
        _asyncDownloader = asyncDownloader ?? _downloader as IAsyncDownloader;
        _serviceUrls = serviceUrls ?? DefaultServiceUrls;
    }

    public string Resolve()
    {
        var result = "";
        foreach (var url in _serviceUrls)
        {
            result = ResolveViaWebService(url);
            if (result.IsMatch(RegExLib.IpV4Address))
                break;
        }
        if (!result.IsMatch(RegExLib.IpV4Address))
            throw new InvalidOperationException("failed to resolve a public ip address");

        return result;
    }

    public async Task<string> ResolveAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_asyncDownloader == null)
            throw new InvalidOperationException("an async downloader is required to resolve a public IP asynchronously");

        foreach (var url in _serviceUrls)
        {
            string result;
            try
            {
                result = await _asyncDownloader.DownloadToStringAsync(url, cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException)
            {
                continue;
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                continue;
            }

            result = result.Replace("\n", "").Trim();
            if (result.IsMatch(RegExLib.IpV4Address))
                return result;
        }

        throw new InvalidOperationException("failed to resolve a public ip address");
    }

    // exposed public for testing purposes, not intended for direct usage 
    public string ResolveViaWebService(string url)
    {
        return _downloader.TryDownloadToString(url, out var result, "") 
            ? result.Replace("\n", "").Trim() 
            : result;
    }

    private readonly IDownloader _downloader;
    private readonly IAsyncDownloader? _asyncDownloader;

    private readonly string[] _serviceUrls;
            
    public static readonly string[] DefaultServiceUrls =
    {
        "https://ipinfo.io/ip",
        "https://checkip.amazonaws.com/",
        "https://api.ipify.org",
        "https://icanhazip.com",
        "https://wtfismyip.com/text"
    };
}
