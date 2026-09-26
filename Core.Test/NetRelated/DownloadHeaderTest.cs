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

    private sealed class StubHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _respond;

        public StubHandler(Func<HttpRequestMessage, HttpResponseMessage> respond)
        {
            _respond = respond;
        }

        public HttpRequestMessage? LastRequest { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(_respond(request));
        }
    }

    private static void WithFakeServer(StubHandler handler, Action action)
    {
        var original = HttpChannelExt.SharedHttpClient;
        HttpChannelExt.SharedHttpClient = new Lazy<HttpClient>(() => new HttpClient(handler));
        try
        {
            action();
        }
        finally
        {
            HttpChannelExt.SharedHttpClient = original;
        }
    }

    [Fact]
    public void ReadsTheValuesOfTheResponseHeaders()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());

        WithFakeServer(handler, () =>
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

        WithFakeServer(handler, () =>
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

        WithFakeServer(handler, () =>
        {
            var header = new DefaultHttpChannel().DownloadHeader("https://example.com/file");

            Assert.Equal("first, second", Assert.IsType<HttpHeader>(header).RawDictionary["X-Multi"]);
        });
    }

    [Fact]
    public void NoValueIsTheNameOfAType()
    {
        var handler = new StubHandler(_ => CreateHeadResponse());

        WithFakeServer(handler, () =>
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

        WithFakeServer(handler, () =>
        {
            new DefaultHttpChannel().DownloadHeader("https://example.com/file", authorization);

            Assert.Equal(HttpMethod.Head, handler.LastRequest!.Method);
            Assert.Equal(authorization, handler.LastRequest.Headers.Authorization);
        });
    }
}
