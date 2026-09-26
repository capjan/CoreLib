using System;
using System.Collections.Generic;
using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated;

public class HttpHeaderTest
{
    [Theory]
    [InlineData("Content-Length", "Content-Type")]
    [InlineData("content-length", "content-type")] // HTTP/2 sends all header names in lower case
    [InlineData("CONTENT-LENGTH", "CONTENT-TYPE")]
    public void HeaderNamesAreCaseInsensitive(string lengthKey, string typeKey)
    {
        var raw = new Dictionary<string, string>
        {
            { lengthKey, "42" },
            { typeKey, "text/html" },
        };

        var header = new HttpHeader(raw);

        Assert.Equal(42, header.ContentLength);
        Assert.Equal("text/html", header.ContentType);
    }

    [Fact]
    public void RawDictionaryIsTheGivenDictionary()
    {
        var raw = new Dictionary<string, string> { { "content-length", "42" } };

        var header = new HttpHeader(raw);

        Assert.Same(raw, header.RawDictionary);
    }

    [Fact]
    public void ParsesDatesIndependentOfTheCurrentCulture()
    {
        var raw = new Dictionary<string, string>
        {
            { "date", "Tue, 15 Nov 1994 08:12:31 GMT" },
            { "last-modified", "Wed, 21 Oct 2015 07:28:00 GMT" },
        };

        var header = new HttpHeader(raw);

        Assert.Equal(new DateTime(1994, 11, 15, 8, 12, 31, DateTimeKind.Utc), header.CreatedAtUtc);
        Assert.Equal(new DateTime(2015, 10, 21, 7, 28, 0, DateTimeKind.Utc), header.LastModifiedUtc);
    }

    [Fact]
    public void SetCookiesIsEmptyWithoutTheHeader()
    {
        var header = new HttpHeader(new Dictionary<string, string>());

        Assert.Null(header.SetCookie);
        Assert.Empty(header.SetCookies);
    }

    [Fact]
    public void SetCookiesWithOneCookie()
    {
        var header = new HttpHeader(new Dictionary<string, string> { { "set-cookie", "a=1; Path=/" } });

        Assert.Equal("a=1; Path=/", header.SetCookie);
        Assert.Equal(new[] { "a=1; Path=/" }, header.SetCookies);
    }

    [Fact]
    public void SetCookiesSplitsAtTheSeparatorAndNotAtCommas()
    {
        const string first = "a=1; Expires=Wed, 21 Oct 2015 07:28:00 GMT";
        const string second = "b=2";
        var raw = new Dictionary<string, string> { { HttpHeader.SetCookieKey, first + HttpHeader.SetCookieSeparator + second } };

        var header = new HttpHeader(raw);

        Assert.Equal(new[] { first, second }, header.SetCookies);
    }
}
