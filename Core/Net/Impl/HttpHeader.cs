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
    /// Initializes the minimal information. Used for http responses from servers that doesn't support http headers
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="contentLength"></param>
    public HttpHeader(string contentType, long contentLength) : this( new Dictionary<string, string>()
    {
        { ContentTypeKey, contentType },
        { ContentLengthKey, contentLength.ToString("D") }
    })
    { }

    public HttpHeader(IReadOnlyDictionary<string,string> headerDict)
    {
        RawDictionary = headerDict;

        if (headerDict.TryGetValue(AcceptRangesKey, out var acceptRangesValue))
            AcceptRanges = acceptRangesValue == "bytes";

        if (headerDict.TryGetValue(ConnectionKey, out var connectionValue))
            Connection = connectionValue;

        if (headerDict.TryGetValue(ContentLengthKey, out var lengthValue))
            ContentLength = long.Parse(lengthValue);

        if (headerDict.TryGetValue(ContentTypeKey, out var contentTypeValue))
            ContentType = contentTypeValue;

        if (headerDict.TryGetValue(DateKey, out var dateValue))
            CreatedAtUtc = DateTime.Parse(dateValue, null, DateTimeStyles.AdjustToUniversal);

        if (headerDict.TryGetValue(ETagKey, out var eTagValue))
            EntityTag = eTagValue;

        if (headerDict.TryGetValue(LastModifiedKey, out var lastModifiedValue))
            LastModifiedUtc = DateTime.Parse(lastModifiedValue, null, DateTimeStyles.AdjustToUniversal);

        if (headerDict.TryGetValue(LocationKey, out var locationValue))
            Location = locationValue;

        if (headerDict.TryGetValue(ServerKey, out var serverValue))
            Server = serverValue;

        if (headerDict.TryGetValue(SetCookieKey, out var setCookieValue))
            SetCookie = setCookieValue;

        if (headerDict.TryGetValue(StatusKey, out var statusValue))
            Status = statusValue;
    }

    public bool? AcceptRanges { get; }

    public string? Connection { get; }

    public string? ContentType { get; }

    public string? Status { get; }

    public long? ContentLength { get; }

    public DateTime? CreatedAtUtc { get; }

    public string? Server { get; }

    public string? SetCookie { get; }

    public string? EntityTag { get; }

    public DateTime? LastModifiedUtc { get; }

    public string? Location { get; }

    public IReadOnlyDictionary<string,string> RawDictionary { get; }

}