using Core.Extensions.NetRelated;
using Core.Net.Impl;
using Core.Text;
using Xunit;

namespace Core.Test.NetRelated
{
    public class HttpChannelTest
    {
        [Fact]
        public void BasicDownload()
        {
            var client = new DefaultHttpChannel();
            var site = client.DownloadToString("https://ipinfo.io/ip").Trim('\n', ' ', '\r');
            Assert.Matches(RegExLib.IpV4Address, site);
        }
    }
}
