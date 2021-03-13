using System;
using System.Text;
using Core.Enums;
using Core.Environment.OperatingSystemInfoImpl.Details;
using Core.Environment.OperatingSystemInfoImpl.Details.LinuxDetection;
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
        public OperatingSystemKind Platform { get; }

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

        public OperatingSystemInfo(IOperatingSystemResolver operatingSystemResolver = default, IOperatingSystemDetailsResolver detailsResolver = default)
        {
            operatingSystemResolver = operatingSystemResolver ?? new OperatingSystemResolver();
            Platform         = operatingSystemResolver.Detect();
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

        private static Lazy<IOperatingSystemDetailsResolver> InitializeDetailsResolver(IOperatingSystemDetailsResolver givenResolver, OperatingSystemKind operatingPlatform)
        {
            if (givenResolver != null)
                return new Lazy<IOperatingSystemDetailsResolver>(() => givenResolver);
            
            switch (operatingPlatform)
            {
                case OperatingSystemKind.MacOS:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new MacOperatingSystemDetailsResolver());
                case OperatingSystemKind.Unknown:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new NullOperatingSystemDetailsResolver());
                case OperatingSystemKind.Windows:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new WindowsDetailsResolver());
                case OperatingSystemKind.Linux:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new LinuxDetailsResolver());
                default:
                    return new Lazy<IOperatingSystemDetailsResolver>(() => new NullOperatingSystemDetailsResolver());
            }
            
        }
    }
}