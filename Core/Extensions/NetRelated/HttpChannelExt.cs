using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Core.ControlFlow;
using Core.Net;
using Core.Net.Impl;

namespace Core.Extensions.NetRelated;

public static class HttpChannelExt
{

    public static Lazy<HttpClient> SharedHttpClient = new Lazy<HttpClient>(() => DefaultHttpClient.Shared.Value);

    /// <summary>
    /// Downloads the content of the url as text.
    /// </summary>
    /// <param name="encoding">
    /// The encoding of the text. If it is not given, the charset the server announces is used (UTF-8 if there is none).
    /// A byte order mark in the content takes precedence over both.
    /// </param>
    /// <exception cref="HttpRequestException">The server did not answer with a success status code.</exception>
    public static string DownloadToString(this IHttpChannel channel, string url, Encoding? encoding = default, AuthenticationHeaderValue? authenticationHeaderValue = null)
    {
        using var request = channel.CreateRequest(url);
        if (authenticationHeaderValue != null)
            request.Headers.Authorization = authenticationHeaderValue;

        using var result = SharedHttpClient.Value.SendAsync(request).GetAwaiter().GetResult();
        result.EnsureSuccessStatusCode();

        if (encoding == null)
            return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        using var stream = result.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        using var reader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true);
        return reader.ReadToEnd();
    }

    public static IHttpHeader DownloadHeader(this IHttpChannel channel, string url, AuthenticationHeaderValue? authenticationHeaderValue = null)
    {
        var request = channel.CreateRequest(url);

        if (authenticationHeaderValue != null)
            request.Headers.Authorization = authenticationHeaderValue;

        request.Method = HttpMethod.Head;

        using var result = SharedHttpClient.Value.SendAsync(request).GetAwaiter().GetResult();

        // Header names are case-insensitive. Content-Length, Content-Type, Last-Modified etc. are content headers,
        // all others response headers, so both collections are needed. A header can have several values.
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        AddHeaders(dict, result.Headers);
        if (result.Content != null)
            AddHeaders(dict, result.Content.Headers);
        return new HttpHeader(dict);
    }

    private static void AddHeaders(Dictionary<string, string> target, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
    {
        foreach (var header in headers)
        {
            // several values are joined with a comma; Set-Cookie is the exception, see HttpHeader.SetCookieSeparator
            var separator = string.Equals(header.Key, HttpHeader.SetCookieKey, StringComparison.OrdinalIgnoreCase)
                ? HttpHeader.SetCookieSeparator
                : ", ";
            target[header.Key] = string.Join(separator, header.Value);
        }
    }

    public static bool TryDownloadHeader(this IHttpChannel channel, string url, out IHttpHeader header)
    {
        header = new HttpHeader("plain", 0);
        var success = new Tryify<IHttpHeader?>()
            .TryInvoke(() => channel.DownloadHeader(url), out var result, fallback: default);
        if (success)
        {
            header = result!;
            return true;
        }

        return false;
    }
}