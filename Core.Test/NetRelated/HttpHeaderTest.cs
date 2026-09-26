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
}
