using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated;

/// <summary>
/// Tests <see cref="HttpChannelExt.DownloadHeader"/> against a fake server, so no internet access is needed.
/// </summary>
[Collection(SharedHttpClientCollection.Name)]
public class DownloadHeaderTest
{
    private static readonly DateTimeOffset LastModified = new(2015, 10, 21, 7, 28, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset Date = new(1994, 11, 15, 8, 12, 31, TimeSpan.Zero);

    /// <summary>
    /// A response like a real server answers a HEAD request: Content-* headers belong to the content headers,
    /// all others to the response headers.
    /// </summary>
    private static HttpResponseMessage CreateHeadResponse()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(Array.Empty<byte>())
        };
        response.Content.Headers.ContentLength = 1234;
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain") { CharSet = "utf-8" };
        response.Content.Headers.LastModified = LastModified;

        response.Headers.Server.ParseAdd("TestServer/1.0");
        response.Headers.ETag = new EntityTagHeaderValue("\"abc\"");
        response.Headers.AcceptRanges.Add("bytes");
        response.Headers.Date = Date;
        response.Headers.Location = new Uri("https://example.com/moved");
        response.Headers.TryAddWithoutValidation("X-Multi", new[] { "first", "second" });
        return response;
    }

    [Fact]
    public void ReadsTheValuesOfTheResponseHeaders()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());

        SharedHttpClientSwap.Use(handler, () =>
        {
            var header = new DefaultHttpChannel().DownloadHeader("https://example.com/file");

            Assert.Equal("TestServer/1.0", header.Server);
            Assert.Equal("\"abc\"", header.EntityTag);
            Assert.True(header.AcceptRanges);
            Assert.Equal("https://example.com/moved", header.Location);
            Assert.Equal(Date.UtcDateTime, header.CreatedAtUtc);
        });
    }

    [Fact]
    public void ReadsTheContentHeaders()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());

        SharedHttpClientSwap.Use(handler, () =>
        {
            var header = new DefaultHttpChannel().DownloadHeader("https://example.com/file");

            Assert.Equal(1234, header.ContentLength);
            Assert.Equal("text/plain; charset=utf-8", header.ContentType);
            Assert.Equal(LastModified.UtcDateTime, header.LastModifiedUtc);
        });
    }

    [Fact]
    public void JoinsMultipleValuesOfOneHeader()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());

        SharedHttpClientSwap.Use(handler, () =>
        {
            var header = new DefaultHttpChannel().DownloadHeader("https://example.com/file");

            Assert.Equal("first, second", Assert.IsType<HttpHeader>(header).RawDictionary["X-Multi"]);
        });
    }

    [Fact]
    public void NoValueIsTheNameOfAType()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());

        SharedHttpClientSwap.Use(handler, () =>
        {
            var header = new DefaultHttpChannel().DownloadHeader("https://example.com/file");

            var raw = Assert.IsType<HttpHeader>(header).RawDictionary;
            Assert.NotEmpty(raw);
            foreach (var pair in raw)
                Assert.DoesNotContain("System.", pair.Value);
        });
    }

    [Fact]
    public void SendsAHeadRequestWithTheAuthorization()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());
        var authorization = new AuthenticationHeaderValue("Bearer", "token");

        SharedHttpClientSwap.Use(handler, () =>
        {
            new DefaultHttpChannel().DownloadHeader("https://example.com/file", authorization);

            Assert.Equal(HttpMethod.Head, handler.LastRequest!.Method);
            Assert.Equal(authorization, handler.LastRequest.Headers.Authorization);
        });
    }

    [Fact]
    public void KeepsSeveralSetCookieHeadersApart()
    {
        // a comma is part of a valid cookie (Expires), so the cookies must not be joined with a comma
        const string first = "a=1; Path=/; Expires=Wed, 21 Oct 2015 07:28:00 GMT";
        const string second = "b=2; Path=/";
        var handler = new StubHandler(_ =>
        {
            var response = CreateHeadResponse();
            response.Headers.TryAddWithoutValidation("Set-Cookie", new[] { first, second });
            return response;
        });

        SharedHttpClientSwap.Use(handler, () =>
        {
            var header = new DefaultHttpChannel().DownloadHeader("https://example.com/file");

            var httpHeader = Assert.IsType<HttpHeader>(header);
            var expected = first + HttpHeader.SetCookieSeparator + second;
            Assert.Equal(expected, header.SetCookie);
            Assert.Equal(expected, httpHeader.RawDictionary["Set-Cookie"]);
            Assert.Equal(new[] { first, second }, httpHeader.SetCookies);

            // every other header with several values is still joined with a comma
            Assert.Equal("first, second", httpHeader.RawDictionary["X-Multi"]);
        });
    }
}
