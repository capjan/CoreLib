using System;
using System.Collections.Generic;
using System.Globalization;

namespace Core.Net.Impl;

public class HttpHeader : IHttpHeader
{
    public const string AcceptRangesKey  = "Accept-Ranges";
    public const string ConnectionKey    = "Connection";
    public const string ContentLengthKey = "Content-Length";
    public const string ContentTypeKey   = "Content-Type";
    public const string DateKey          = "Date";
    public const string ETagKey          = "ETag";
    public const string LastModifiedKey  = "Last-Modified";
    public const string LocationKey      = "Location";
    public const string ServerKey        = "Server";
    public const string SetCookieKey     = "Set-Cookie";

    // non standard but read somewhere
    public const string StatusKey = "Status";

    /// <summary>
    /// Separates several Set-Cookie values in <see cref="SetCookie"/> and in <see cref="RawDictionary"/>.
    /// Other headers with several values are joined with a comma, but Set-Cookie must not be (RFC 6265, section 3;
    /// RFC 9110, section 5.3): a comma can be part of a cookie, e.g. in <c>Expires=Wed, 21 Oct 2015 07:28:00 GMT</c>.
    /// A line feed can never be part of a header value, so it keeps the cookies apart without loss.
    /// </summary>
    public const string SetCookieSeparator = "\n";

    /// <summary>
    /// Initializes the minimal information. Used for http responses from servers that doesn't support http headers
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="contentLength"></param>
    public HttpHeader(string contentType, long contentLength) : this( new Dictionary<string, string>()
    {
        { ContentTypeKey, contentType },
        { ContentLengthKey, contentLength.ToString("D", CultureInfo.InvariantCulture) }
    })
    { }

    public HttpHeader(IReadOnlyDictionary<string,string> headerDict)
    {
        RawDictionary = headerDict;

        // header names are case-insensitive, e.g. HTTP/2 sends them in lower case
        var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var pair in headerDict)
            headers[pair.Key] = pair.Value;

        if (headers.TryGetValue(AcceptRangesKey, out var acceptRangesValue))
            AcceptRanges = acceptRangesValue == "bytes";

        if (headers.TryGetValue(ConnectionKey, out var connectionValue))
            Connection = connectionValue;

        if (headers.TryGetValue(ContentLengthKey, out var lengthValue))
            ContentLength = long.Parse(lengthValue, NumberStyles.Integer, CultureInfo.InvariantCulture);

        if (headers.TryGetValue(ContentTypeKey, out var contentTypeValue))
            ContentType = contentTypeValue;

        if (headers.TryGetValue(DateKey, out var dateValue))
            CreatedAtUtc = DateTime.Parse(dateValue, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

        if (headers.TryGetValue(ETagKey, out var eTagValue))
            EntityTag = eTagValue;

        if (headers.TryGetValue(LastModifiedKey, out var lastModifiedValue))
            LastModifiedUtc = DateTime.Parse(lastModifiedValue, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

        if (headers.TryGetValue(LocationKey, out var locationValue))
            Location = locationValue;

        if (headers.TryGetValue(ServerKey, out var serverValue))
            Server = serverValue;

        if (headers.TryGetValue(SetCookieKey, out var setCookieValue))
        {
            SetCookie = setCookieValue;
            SetCookies = setCookieValue.Split(SetCookieSeparatorChars, StringSplitOptions.RemoveEmptyEntries);
        }

        if (headers.TryGetValue(StatusKey, out var statusValue))
            Status = statusValue;
    }

    public bool? AcceptRanges { get; }

    public string? Connection { get; }

    public string? ContentType { get; }

    public string? Status { get; }

    public long? ContentLength { get; }

    public DateTime? CreatedAtUtc { get; }

    public string? Server { get; }

    /// <summary>
    /// All Set-Cookie values, joined with <see cref="SetCookieSeparator"/>.
    /// </summary>
    public string? SetCookie { get; }

    /// <summary>
    /// The Set-Cookie values, one entry per cookie. Empty if the header is not present.
    /// </summary>
    public IReadOnlyList<string> SetCookies { get; } = Array.Empty<string>();

    private static readonly char[] SetCookieSeparatorChars = SetCookieSeparator.ToCharArray();

    public string? EntityTag { get; }

    public DateTime? LastModifiedUtc { get; }

    public string? Location { get; }

    public IReadOnlyDictionary<string,string> RawDictionary { get; }

}