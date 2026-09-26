using System.Net;
using System.Net.Http;
using System.Text;
using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated;

/// <summary>
/// Tests <see cref="HttpChannelExt.DownloadToString"/> and the downloader on top of it against a fake server.
/// </summary>
[Collection(SharedHttpClientCollection.Name)]
public class DownloadToStringTest
{
    private const string Url = "https://example.com/text";
    private const string Text = "Grüße aus Köln";
    private static readonly Encoding Latin1 = Encoding.GetEncoding("ISO-8859-1");

    private static HttpResponseMessage Response(byte[] body, string? contentType, HttpStatusCode status = HttpStatusCode.OK)
    {
        var response = new HttpResponseMessage(status) { Content = new ByteArrayContent(body) };
        if (contentType != null)
            response.Content.Headers.TryAddWithoutValidation("Content-Type", contentType);
        return response;
    }

    private static StubHandler Serve(byte[] body, string? contentType, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new StubHandler(_ => Response(body, contentType, status));
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public void ThrowsForAnErrorStatusInsteadOfReturningTheErrorPage(HttpStatusCode status)
    {
        var handler = Serve(Encoding.UTF8.GetBytes("<html>error page</html>"), "text/html", status);

        SharedHttpClientSwap.Use(handler, () =>
        {
            var exception = Assert.Throws<HttpRequestException>(() => new DefaultHttpChannel().DownloadToString(Url));
            Assert.Contains(((int)status).ToString(), exception.Message);
        });
    }

    [Fact]
    public void TryDownloadToStringFailsForAnErrorStatus()
    {
        var handler = Serve(Encoding.UTF8.GetBytes("<html>error page</html>"), "text/html", HttpStatusCode.NotFound);

        SharedHttpClientSwap.Use(handler, () =>
        {
            var downloader = new HttpChannelDownloader();

            Assert.False(downloader.TryDownloadToString(Url, out var result, "fallback"));
            Assert.Equal("fallback", result);
        });
    }

    [Fact]
    public void UsesTheCharsetOfTheServerWithoutAnExplicitEncoding()
    {
        var handler = Serve(Latin1.GetBytes(Text), "text/plain; charset=iso-8859-1");

        SharedHttpClientSwap.Use(handler, () =>
            Assert.Equal(Text, new DefaultHttpChannel().DownloadToString(Url)));
    }

    [Fact]
    public void UsesTheExplicitEncoding()
    {
        // the server does not say which charset it uses, so the default (UTF-8) would garble the text
        var handler = Serve(Latin1.GetBytes(Text), "text/plain");

        SharedHttpClientSwap.Use(handler, () =>
            Assert.Equal(Text, new DefaultHttpChannel().DownloadToString(Url, Latin1)));
    }

    [Fact]
    public void TheExplicitEncodingWinsOverTheCharsetOfTheServer()
    {
        var handler = Serve(Encoding.UTF8.GetBytes(Text), "text/plain; charset=iso-8859-1");

        SharedHttpClientSwap.Use(handler, () =>
            Assert.Equal(Text, new DefaultHttpChannel().DownloadToString(Url, Encoding.UTF8)));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void TheByteOrderMarkIsNotPartOfTheText(bool explicitEncoding)
    {
        var body = new byte[] { 0xEF, 0xBB, 0xBF };
        body = Concat(body, Encoding.UTF8.GetBytes(Text));
        var handler = Serve(body, "text/plain; charset=utf-8");

        SharedHttpClientSwap.Use(handler, () =>
        {
            var text = new DefaultHttpChannel().DownloadToString(Url, explicitEncoding ? Encoding.UTF8 : null);
            Assert.Equal(Text, text);
        });
    }

    [Fact]
    public void AByteOrderMarkWinsOverTheExplicitEncoding()
    {
        // the caller says Latin-1, but the content is UTF-8 with a byte order mark: the mark tells the truth
        var body = Concat(new byte[] { 0xEF, 0xBB, 0xBF }, Encoding.UTF8.GetBytes(Text));
        var handler = Serve(body, "text/plain");

        SharedHttpClientSwap.Use(handler, () =>
            Assert.Equal(Text, new DefaultHttpChannel().DownloadToString(Url, Latin1)));
    }

    [Fact]
    public void HttpChannelDownloaderUsesTheCharsetOfTheServerByDefault()
    {
        var handler = Serve(Latin1.GetBytes(Text), "text/plain; charset=iso-8859-1");

        SharedHttpClientSwap.Use(handler, () =>
            Assert.Equal(Text, new HttpChannelDownloader().DownloadToString(Url)));
    }

    [Fact]
    public void HttpChannelDownloaderUsesTheGivenEncoding()
    {
        var handler = Serve(Latin1.GetBytes(Text), "text/plain");

        SharedHttpClientSwap.Use(handler, () =>
            Assert.Equal(Text, new HttpChannelDownloader(encoding: Latin1).DownloadToString(Url)));
    }

    private static byte[] Concat(byte[] first, byte[] second)
    {
        var result = new byte[first.Length + second.Length];
        first.CopyTo(result, 0);
        second.CopyTo(result, first.Length);
        return result;
    }
}
