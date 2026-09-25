using System;

namespace Core.Environment.OperatingSystemInfoImpl.Details.MacOSDetection;

public class MacOperatingSystemDetailsResolver : IOperatingSystemDetailsResolver
{
    private readonly Lazy<MacOperatingSystemVersion> _swVersion = new Lazy<MacOperatingSystemVersion>(() => new MacOperatingSystemVersion());

    public string ResolveVersion()
    {
        return _swVersion.Value.ProductVersion;
    }

    public string ResolveBuildVersion()
    {
        return _swVersion.Value.BuildVersion;
    }

    public string ResolveName()
    {
        var version = _swVersion.Value.ProductVersion;
        if (version.StartsWith("26.", StringComparison.Ordinal))
            return "macOS Tahoe";
        if (version.StartsWith("15.", StringComparison.Ordinal))
            return "macOS Sequoia";
        if (version.StartsWith("14.", StringComparison.Ordinal))
            return "macOS Sonoma";
        if (version.StartsWith("13.", StringComparison.Ordinal))
            return "macOS Ventura";
        if (version.StartsWith("12.", StringComparison.Ordinal))
            return "macOS Monterey";
        if (version.StartsWith("11.", StringComparison.Ordinal))
            return "macOS Big Sur";
        if (version.StartsWith("10.15", StringComparison.Ordinal))
            return "macOS Catalina";
        if (version.StartsWith("10.14", StringComparison.Ordinal))
            return "macOS Mojave";
        if (version.StartsWith("10.13", StringComparison.Ordinal))
            return "macOS High Sierra";
        if (version.StartsWith("10.12", StringComparison.Ordinal))
            return "macOS Sierra";
        if (version.StartsWith("10.11", StringComparison.Ordinal))
            return "OS X El Capitan";
        if (version.StartsWith("10.10", StringComparison.Ordinal))
            return "OS X Yosemite";
        if (version.StartsWith("10.9", StringComparison.Ordinal))
            return "OS X Mavericks";
        if (version.StartsWith("10.8", StringComparison.Ordinal))
            return "OS X Mountain Lion";
        if (version.StartsWith("10.7", StringComparison.Ordinal))
            return "OS X Lion";
        if (version.StartsWith("10.6", StringComparison.Ordinal))
            return "Mac OS X Snow Leopard";
        if (version.StartsWith("10.5", StringComparison.Ordinal))
            return "Mac OS X Leopard";
        if (version.StartsWith("10.4", StringComparison.Ordinal))
            return "Mac OS X Tiger";
        if (version.StartsWith("10.3", StringComparison.Ordinal))
            return "Mac OS X Panther";
        if (version.StartsWith("10.1", StringComparison.Ordinal))
            return "Mac OS X Puma";
        if (version.StartsWith("10.0", StringComparison.Ordinal))
            return "Mac OS X Cheetah";

        // unknown (e.g. newer) release: fall back to the product name reported by sw_vers instead of an empty name
        var productName = _swVersion.Value.ProductName;
        return string.IsNullOrEmpty(productName) ? "macOS" : productName;
    }
}