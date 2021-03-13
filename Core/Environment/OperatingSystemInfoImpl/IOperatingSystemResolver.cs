using System.Runtime.InteropServices;
using Core.Enums;

namespace Core.Environment.OperatingSystemInfoImpl
{
    public interface IOperatingSystemResolver
    {
        OperatingSystemKind Detect();
    }

    /// <summary>
    /// Default OS System Resolver
    /// </summary>
    public class OperatingSystemResolver: IOperatingSystemResolver
    {
        public OperatingSystemKind Detect()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OperatingSystemKind.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OperatingSystemKind.MacOS;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OperatingSystemKind.Linux;
            return OperatingSystemKind.Unknown;
        }
    }
}
