using Core.Enums;
using Core.Environment.OperatingSystemInfoImpl;
using Xunit;

namespace Core.Test.EnvironmentRelated;

public class OSSystemResolverTest
{
    [Fact]
    public void BasicTest()
    {
        var resolver = new OperatingSystemResolver();
        var detectedOS       = resolver.Detect();
        Assert.NotEqual(OperatingSystemKind.Unknown, detectedOS);
    }

    [Fact]
    public void ResolverMatchesOsInfo()
    {
        var resolver   = new OperatingSystemResolver();
        var detectedOS = resolver.Detect();
        var osInfo = new OperatingSystemInfo();

        Assert.Equal(detectedOS, osInfo.Platform);
    }
}