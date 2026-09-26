using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Core.Extensions.NetRelated;

namespace Core.Net.Impl;

public class DefaultDownloader : IDownloader
{
    // one client for all downloaders: a client per download exhausts sockets and is slow
    private static readonly Lazy<HttpClient> SharedClient = new Lazy<HttpClient>(() => DefaultHttpClient.Create());

    private readonly HttpClient? _client;

    /// <summary>
    /// Downloads with a client that is shared by all downloaders of the application.
    /// </summary>
    public DefaultDownloader()
    {
    }

    /// <summary>
    /// Downloads with the given client. The client is not disposed, its lifetime is up to the caller.
    /// </summary>
    public DefaultDownloader(HttpClient httpClient)
    {
        _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public string DownloadToString(string url)
    {
        return (_client ?? SharedClient.Value).GetStringAsync(url).Result;
    }
}

public class DownloaderWithCredentials : IDownloader
{
    private readonly Lazy<HttpClient> _client;

    /// <summary>
    /// The client is created with the first download and then reused by this instance.
    /// </summary>
    public DownloaderWithCredentials(ICredentials credentials)
    {
        _client = new Lazy<HttpClient>(() => DefaultHttpClient.Create(credentials));
    }

    public string DownloadToString(string url)
    {
        return _client.Value.GetStringAsync(url).Result;
    }
}

// other implementations
public class HttpChannelDownloader : IDownloader
{
    public HttpChannelDownloader(
        IHttpChannel? httpChannel = default, 
        Encoding? encoding = default)
    {
        _httpChannel = httpChannel ?? new DefaultHttpChannel();
        _encoding = encoding;
    }

    public string DownloadToString(string url)
    {
        return _httpChannel.DownloadToString(url, _encoding);
    }

    private readonly Encoding?    _encoding;
    private readonly IHttpChannel _httpChannel;
}