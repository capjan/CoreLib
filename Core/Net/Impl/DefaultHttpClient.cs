using System;
using System.Net;
using System.Net.Http;

namespace Core.Net.Impl;

/// <summary>
/// Creates the <see cref="HttpClient"/> that is meant to live as long as the application.
/// </summary>
internal static class DefaultHttpClient
{
    /// <summary>
    /// The client all downloaders and <c>HttpChannelExt.SharedHttpClient</c> start with, so they share one connection pool.
    /// It does not store cookies.
    /// </summary>
    public static readonly Lazy<HttpClient> Shared = new Lazy<HttpClient>(() => Create());

    /// <param name="credentials">The credentials for servers that ask for authentication.</param>
    /// <param name="useCookies">
    /// Whether the client stores the cookies of the servers and sends them with later requests.
    /// A client that is shared or reused must not do that unless all its users are meant to share one session,
    /// so it is off by default.
    /// </param>
    public static HttpClient Create(ICredentials? credentials = null, bool useCookies = false)
    {
#if NETSTANDARD2_0
        return new HttpClient(new HttpClientHandler { Credentials = credentials, UseCookies = useCookies });
#else
        // connections are recycled regularly, so that a long-lived client notices changed DNS entries
        return new HttpClient(new SocketsHttpHandler
        {
            Credentials = credentials,
            UseCookies = useCookies,
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        });
#endif
    }
}
