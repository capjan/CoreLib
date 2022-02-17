using Core.Net.Impl;
using Core.Text;
using Xunit;

namespace Core.Test.NetRelated;

public class PublicIpResolverTest
{
    [Fact]
    public void ResolveTest()
    {
        var ipResolver = new DefaultPublicIpResolver();
        Assert.Matches(RegExLib.IpV4Address, ipResolver.Resolve());
    }

    [Fact]
    public void DefaultServiceUrlTest()
    {
        var resolver = new DefaultPublicIpResolver();
        var resolvedIp = resolver.Resolve();
        Assert.NotNull(resolvedIp);
        Assert.Matches(RegExLib.IpV4Address, resolvedIp);

        foreach (var serviceUrl in DefaultPublicIpResolver.DefaultServiceUrls)
        {
            var ipViaService = resolver.ResolveViaWebService(serviceUrl);
            Assert.Matches(RegExLib.IpV4Address, ipViaService);
            Assert.Equal(resolvedIp, ipViaService);
        }
    }

    [Fact]
    public void ResolveViaCustomService()
    {
        var serviceUrls = new [] {"https://api.ipify.org"};
        var resolver    = new DefaultPublicIpResolver(serviceUrls: serviceUrls);
        var ip          = resolver.Resolve();
        Assert.NotNull(ip);
    }
}