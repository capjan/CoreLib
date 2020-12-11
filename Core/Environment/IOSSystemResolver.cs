using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Environment
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
                return OSSystem.OSX;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OSSystem.Linux;
            return OSSystem.Unknown;
        }
    }
}
