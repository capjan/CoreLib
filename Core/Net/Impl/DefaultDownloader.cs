using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Core.Extensions.NetRelated;

namespace Core.Net.Impl;

public class DefaultDownloader : IDownloader
{
    // One client for all downloaders: a client per download exhausts sockets and is slow.
    // It does not keep cookies: every download stands for its own user or job and must not see the session of another.
    private static Lazy<HttpClient> SharedClient => DefaultHttpClient.Shared;

    private readonly HttpClient? _client;

    /// <summary>
    /// Downloads with a client that is shared by all downloaders of the application. It does not store cookies.
    /// </summary>
    public DefaultDownloader()
    {
    }

    /// <summary>
    /// Downloads with the given client. The client is not disposed, its lifetime is up to the caller.
    /// Use this for downloads that need cookies: the client decides whether they are stored and sent.
    /// </summary>
    public DefaultDownloader(HttpClient httpClient)
    {
        _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public string DownloadToString(string url)
    {
        return (_client ?? SharedClient.Value).GetStringAsync(url).GetAwaiter().GetResult();
    }
}

public class DownloaderWithCredentials : IDownloader
{
    private readonly Lazy<HttpClient> _client;

    /// <summary>
    /// The client is created with the first download and then reused by this instance. It does not store cookies.
    /// </summary>
    public DownloaderWithCredentials(ICredentials credentials)
    {
        if (credentials == null) throw new ArgumentNullException(nameof(credentials));

        _client = new Lazy<HttpClient>(() => DefaultHttpClient.Create(credentials));
    }

    public string DownloadToString(string url)
    {
        return _client.Value.GetStringAsync(url).GetAwaiter().GetResult();
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