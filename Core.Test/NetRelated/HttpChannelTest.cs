using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Core.Text;
using Xunit;

namespace Core.Test.NetRelated;

[Trait("Category", "Network")]
[Collection(SharedHttpClientCollection.Name)]
public class HttpChannelTest
{
    [Fact]
    public void BasicDownload()
    {
        var client = new DefaultHttpChannel();
        var site = client.DownloadToString("https://ipinfo.io/ip").Trim('\n', ' ', '\r');
        Assert.Matches(RegExLib.IpV4Address, site);
    }

    [Fact]
    public void DownloadHeaderFromARealServer()
    {
        var header = new DefaultHttpChannel().DownloadHeader("https://www.example.com/");

        Assert.StartsWith("text/html", header.ContentType);
        Assert.NotNull(header.CreatedAtUtc);
    }

    [Fact]
    public void TryDownloadHeaderFromARealServer()
    {
        Assert.True(new DefaultHttpChannel().TryDownloadHeader("https://www.example.com/", out var header));
        Assert.StartsWith("text/html", header.ContentType);
    }
}