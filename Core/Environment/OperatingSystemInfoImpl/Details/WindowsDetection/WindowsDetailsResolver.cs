using System;

namespace Core.Environment.OperatingSystemInfoImpl.Details.WindowsDetection
{
    internal class WindowsDetailsResolver : IOperatingSystemDetailsResolver
    {

        private readonly Lazy<Version> _lazyVersion;

        public WindowsDetailsResolver()
        {
            _lazyVersion = new Lazy<Version>(() => WindowsVerWrapper.GetVersion());
        }

        public string ResolveVersion()
        {
            return _lazyVersion.Value.ToString(2);
        }

        public string ResolveBuildVersion()
        {
            return _lazyVersion.Value.Build.ToString();
        }

        public string ResolveName()
        {
            var version = _lazyVersion.Value;
            switch (version.Major)
            {
                case 10:
                    return "Windows 10";
                case 6:
                    switch (version.Minor)
                    {
                        case 0:
                            return "Windows Vista";
                        case 1:
                            return "Windows 7";
                        case 2:
                            return "Windows 8";
                        case 3:
                            return "Windows 8.1";
                    }

                    return $"Windows {version}";
                case 5:
                    switch (version.Minor)
                    {
                        case 0:
                            return "Windows 2000";
                        case 1:
                            return "Windows XP";
                    }

                    return $"Windows {version}";
                case 4:
                    return "Windows 98";
                default:
                    return $"Windows {version}";
            }
        }
    }
	
}
