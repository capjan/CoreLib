using System.Runtime.InteropServices;
using Core.Enums;

namespace Core.Environment.OperatingSystemInfoImpl
{
    public interface IOSSystemResolver
    {
        OSSystem Detect();
    }

    /// <summary>
    /// Default OS System Resolver
    /// </summary>
    public class OSSystemResolver: IOSSystemResolver
    {
        public OSSystem Detect()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OSSystem.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OSSystem.MacOS;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OSSystem.Linux;
            return OSSystem.Unknown;
        }
    }
}
