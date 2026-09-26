using System;
using System.Net;
using System.Net.Http;

namespace Core.Net.Impl;

/// <summary>
/// Creates the <see cref="HttpClient"/> that is meant to live as long as the application.
/// </summary>
internal static class DefaultHttpClient
{
    public static HttpClient Create(ICredentials? credentials = null)
    {
#if NETSTANDARD2_0
        return new HttpClient(new HttpClientHandler { Credentials = credentials });
#else
        // connections are recycled regularly, so that a long-lived client notices changed DNS entries
        return new HttpClient(new SocketsHttpHandler
        {
            Credentials = credentials,
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        });
#endif
    }
}
