using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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
    public async Task DownloaderWithCredentialsSendsTheCredentialsAndCanBeUsedRepeatedly()
    {
        var port = GetFreePort();
        var url = $"http://127.0.0.1:{port}/secret";
        var seen = new List<string>();

        using var listener = new HttpListener { AuthenticationSchemes = AuthenticationSchemes.Basic };
        listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        listener.Start();

        // the listener answers the first request without credentials with a 401 itself
        var server = Task.Run(async () =>
        {
            for (var i = 0; i < 2; i++)
            {
                var context = await listener.GetContextAsync();
                var identity = (HttpListenerBasicIdentity)context.User!.Identity!;
                lock (seen)
                    seen.Add($"{identity.Name}:{identity.Password}");

                var body = Encoding.UTF8.GetBytes("secret");
                context.Response.ContentLength64 = body.Length;
                await context.Response.OutputStream.WriteAsync(body, 0, body.Length);
                context.Response.Close();
            }
        });

        try
        {
            var downloader = new DownloaderWithCredentials(new NetworkCredential("user", "pass"));

            Assert.Equal("secret", downloader.DownloadToString(url));
            Assert.Equal("secret", downloader.DownloadToString(url));

            await server.WaitAsync(TimeSpan.FromSeconds(10)); // fails with a TimeoutException if a request never arrives
            Assert.Equal(new[] { "user:pass", "user:pass" }, seen);
        }
        finally
        {
            listener.Stop();
        }
    }

    private static int GetFreePort()
    {
        var tcp = new TcpListener(IPAddress.Loopback, 0);
        tcp.Start();
        try
        {
            return ((IPEndPoint)tcp.LocalEndpoint).Port;
        }
        finally
        {
            tcp.Stop();
        }
    }
}
