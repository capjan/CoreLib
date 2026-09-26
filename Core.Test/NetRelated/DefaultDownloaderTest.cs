using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated;

public class DefaultDownloaderTest
{
    private static HttpResponseMessage Text(string text, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new HttpResponseMessage(status) { Content = new StringContent(text, Encoding.UTF8, "text/plain") };
    }

    [Fact]
    public async Task UsesTheGivenClientAndDoesNotDisposeIt()
    {
        var handler = new StubHandler(_ => Text("body"));
        using var client = new HttpClient(handler);
        var downloader = new DefaultDownloader(client);

        Assert.Equal("body", downloader.DownloadToString("https://example.com/a"));
        Assert.Equal("body", downloader.DownloadToString("https://example.com/b"));

        Assert.Equal(2, handler.RequestCount);
        // a disposed client throws an ObjectDisposedException
        Assert.Equal("body", await client.GetStringAsync("https://example.com/c"));
    }

    [Fact]
    public void FailsForAnErrorStatus()
    {
        var handler = new StubHandler(_ => Text("error page", HttpStatusCode.NotFound));
        using var client = new HttpClient(handler);
        var downloader = new DefaultDownloader(client);

        Assert.False(downloader.TryDownloadToString("https://example.com/missing", out var result, "fallback"));
        Assert.Equal("fallback", result);
    }

    [Fact]
    public void TheClientIsRequired()
    {
        Assert.Throws<ArgumentNullException>(() => new DefaultDownloader(null!));
    }

    [Fact]
    public void DownloaderWithCredentialsSendsTheCredentialsAndCanBeUsedRepeatedly()
    {
        var seen = new List<string>();

        // the listener answers a request without credentials with a 401 itself
        using var server = new LocalHttpServer(context =>
        {
            var identity = (HttpListenerBasicIdentity)context.User!.Identity!;
            lock (seen)
                seen.Add($"{identity.Name}:{identity.Password}");
            LocalHttpServer.WriteText(context, "secret");
        }, AuthenticationSchemes.Basic);

        var downloader = new DownloaderWithCredentials(new NetworkCredential("user", "pass"));

        Assert.Equal("secret", downloader.DownloadToString(server.Url("/secret")));
        Assert.Equal("secret", downloader.DownloadToString(server.Url("/secret")));

        lock (seen)
            Assert.Equal(new[] { "user:pass", "user:pass" }, seen);
    }

    [Fact]
    public void ThrowsTheHttpRequestExceptionItselfForAnErrorStatus()
    {
        using var client = new HttpClient(new StubHandler(_ => Text("error page", HttpStatusCode.NotFound)));

        // not wrapped in an AggregateException
        Assert.Throws<HttpRequestException>(() => new DefaultDownloader(client).DownloadToString("https://example.com/missing"));
    }

    [Fact]
    public void ThrowsTheHttpRequestExceptionItselfIfTheServerCannotBeReached()
    {
        using var client = new HttpClient(new StubHandler(_ => throw new HttpRequestException("no route to host")));

        var exception = Assert.Throws<HttpRequestException>(() => new DefaultDownloader(client).DownloadToString("https://example.com/"));
        Assert.Equal("no route to host", exception.Message);
    }

    [Fact]
    public void TheCredentialsAreRequired()
    {
        Assert.Throws<ArgumentNullException>(() => new DownloaderWithCredentials(null!));
    }

    [Fact]
    public void HonorsTheTimeoutOfTheGivenClient()
    {
        using var client = new HttpClient(new HangingHandler()) { Timeout = TimeSpan.FromMilliseconds(100) };

        Assert.ThrowsAny<OperationCanceledException>(() => new DefaultDownloader(client).DownloadToString("https://example.com/slow"));
    }

    [Fact]
    public void AnAlreadyDisposedClientFailsWhenItIsUsed()
    {
        var client = new HttpClient(new StubHandler(_ => Text("body")));
        client.Dispose();

        // the downloader does not own the client, so it does not check it: using it is the caller's job
        Assert.Throws<ObjectDisposedException>(() => new DefaultDownloader(client).DownloadToString("https://example.com/"));
    }

    /// <summary>
    /// Never answers, so only the timeout of the client ends the request.
    /// </summary>
    private sealed class HangingHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            await Task.Delay(System.Threading.Timeout.InfiniteTimeSpan, cancellationToken);
            return new HttpResponseMessage();
        }
    }
}
