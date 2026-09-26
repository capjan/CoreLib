using System.Net;
using System.Net.Http;
using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated;

/// <summary>
/// The downloaders share their <see cref="HttpClient"/> to reuse connections. That must not share state:
/// two independent downloads (users, jobs) must never see each other's cookies.
/// </summary>
[Collection(SharedHttpClientCollection.Name)] // uses the process-wide HttpChannelExt.SharedHttpClient
public class DownloaderCookieTest
{
    /// <summary>
    /// /login sets a session cookie, every other path answers with the cookies the request carried.
    /// </summary>
    private static LocalHttpServer StartServer()
    {
        return new LocalHttpServer(context =>
        {
            if (context.Request.Url!.AbsolutePath == "/login")
            {
                context.Response.AppendHeader("Set-Cookie", "session=first-user; Path=/");
                LocalHttpServer.WriteText(context, "logged in");
            }
            else
            {
                LocalHttpServer.WriteText(context, context.Request.Headers["Cookie"] ?? "no cookie");
            }
        });
    }

    [Fact]
    public void DefaultDownloadersDoNotShareCookies()
    {
        using var server = StartServer();

        Assert.Equal("logged in", new DefaultDownloader().DownloadToString(server.Url("/login")));

        // a different downloader stands for a different user or job
        Assert.Equal("no cookie", new DefaultDownloader().DownloadToString(server.Url("/whoami")));
    }

    [Fact]
    public void DefaultDownloaderDoesNotKeepCookiesBetweenDownloads()
    {
        using var server = StartServer();
        var downloader = new DefaultDownloader();

        downloader.DownloadToString(server.Url("/login"));

        Assert.Equal("no cookie", downloader.DownloadToString(server.Url("/whoami")));
    }

    [Fact]
    public void DownloaderWithCredentialsDoesNotKeepCookies()
    {
        using var server = StartServer();
        var downloader = new DownloaderWithCredentials(new NetworkCredential("user", "pass"));

        downloader.DownloadToString(server.Url("/login"));

        Assert.Equal("no cookie", downloader.DownloadToString(server.Url("/whoami")));
        Assert.Equal("no cookie", new DownloaderWithCredentials(new NetworkCredential("other", "pass")).DownloadToString(server.Url("/whoami")));
    }

    [Fact]
    public void AGivenClientMayKeepItsCookies()
    {
        // stateful downloads are possible on purpose: with a client the caller owns and configures
        using var server = StartServer();
        using var client = new HttpClient(new HttpClientHandler { UseCookies = true });
        var downloader = new DefaultDownloader(client);

        downloader.DownloadToString(server.Url("/login"));

        Assert.Equal("session=first-user", downloader.DownloadToString(server.Url("/whoami")));
    }

    [Fact]
    public void TheDefaultHttpChannelDoesNotKeepCookies()
    {
        // HttpChannelExt.SharedHttpClient is process-wide as well
        using var server = StartServer();
        var channel = new DefaultHttpChannel();

        channel.DownloadToString(server.Url("/login"));

        Assert.Equal("no cookie", channel.DownloadToString(server.Url("/whoami")));
    }
}
