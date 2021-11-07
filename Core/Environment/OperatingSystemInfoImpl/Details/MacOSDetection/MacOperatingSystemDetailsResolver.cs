using System;

namespace Core.Environment.OperatingSystemInfoImpl.Details.MacOSDetection
{
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
            if (version.StartsWith("12."))
                return "macOS Monterey";
            if (version.StartsWith("11."))
                return "macOS Big Sur";
            if (version.StartsWith("10.15"))
                return "macOS Catalina";
            if (version.StartsWith("10.14"))
                return "macOS Mojave";
            if (version.StartsWith("10.13"))
                return "macOS High Sierra";
            if (version.StartsWith("10.12"))
                return "macOS Sierra";
            if (version.StartsWith("10.11"))
                return "OS X El Capitan";
            if (version.StartsWith("10.10"))
                return "OS X Yosemite";
            if (version.StartsWith("10.9"))
                return "OS X Mavericks";
            if (version.StartsWith("10.8"))
                return "OS X Mountain Lion";
            if (version.StartsWith("10.7"))
                return "OS X Lion";
            if (version.StartsWith("10.6"))
                return "Mac OS X Snow Leopard";
            if (version.StartsWith("10.5"))
                return "Mac OS X Leopard";
            if (version.StartsWith("10.4"))
                return "Mac OS X Tiger";
            if (version.StartsWith("10.3"))
                return "Mac OS X Panther";
            if (version.StartsWith("10.1"))
                return "Mac OS X Puma";
            if (version.StartsWith("10.0"))
                return "Mac OS X Cheetah";
            return string.Empty;
        }
    }
}
