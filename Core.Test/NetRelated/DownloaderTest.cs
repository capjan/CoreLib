using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Xunit;

namespace Core.Test.NetRelated;

[Trait("Category", "Network")]
public class DownloaderTest
{
    [Fact]
    public void BasicTest()
    {
        const string url = "https://www.example.com/";

        var downloader = new DefaultDownloader();
        if (!downloader.TryDownloadToString(url, out var result))
        {
            Assert.Fail($"download failed. url: {url}");
        }
        Assert.Contains("Example Domain", result);
    }
}