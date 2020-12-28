﻿using System;
using System.Text;
using Core.Enums;
using Core.Environment.OperatingSystemInfoImpl.Details;
using Core.Environment.OperatingSystemInfoImpl.Details.MacOSDetection;
using Core.Environment.OperatingSystemInfoImpl.Details.NullDetection;
using Core.Environment.OperatingSystemInfoImpl.Details.WindowsDetection;

namespace Core.Environment.OperatingSystemInfoImpl
{
    public class OperatingSystemInfo : IOperatingSystemInfo
    {
        /// <summary>
        /// Return the Platform of the operating system. e.g. Windows, OS X, Linux
        /// </summary>
        public OSSystem Platform { get; }

        /// <summary>
        /// Returns the Name of the Operating System
        /// </summary>
        public string Name => _detailsResolver.Value.ResolveName();

        /// <summary>
        /// Returns the Version of the Operating System as String
        /// </summary>
        public string Version => _detailsResolver.Value.ResolveVersion();

        /// <summary>
        /// Returns the Build Version of the Operating System
        /// </summary>
        public string Build => _detailsResolver.Value.ResolveBuildVersion();

        private readonly Lazy<IOperatingSystemDetailsResolver> _detailsResolver;

        public OperatingSystemInfo(IOSSystemResolver osSystemResolver = default, IOperatingSystemDetailsResolver detailsResolver = default)
        {
            osSystemResolver = osSystemResolver ?? new OSSystemResolver();
            Platform         = osSystemResolver.Detect();
            _detailsResolver = InitializeDetailsResolver(detailsResolver, Platform);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(!string.IsNullOrEmpty(Name) ? Name : $"{Platform}");

            if (!string.IsNullOrEmpty(Version))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append($"Version {Version}");
            }

            if (!string.IsNullOrEmpty(Build))
            {
                if (sb.Length > 0) sb.Append(", ");
                sb.Append($"Build {Build}");
            }

            return sb.ToString();
        }

        private static Lazy<IOperatingSystemDetailsResolver> InitializeDetailsResolver(IOperatingSystemDetailsResolver givenResolver, OSSystem osPlatform)
        {
            if (givenResolver != null)
                return new Lazy<IOperatingSystemDetailsResolver>(() => givenResolver);
            
            switch (osPlatform)
            {
                case OSSystem.MacOS:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new MacOSDetailsResolver());
                case OSSystem.Unknown:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new NullOSDetailsResolver());
                case OSSystem.Windows:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new WindowsDetailsResolver());
                case OSSystem.Linux:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new NullOSDetailsResolver());
                default:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new NullOSDetailsResolver());
            }
            
        }
    }
}