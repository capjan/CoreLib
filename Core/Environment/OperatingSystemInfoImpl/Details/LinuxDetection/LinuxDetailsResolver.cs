using System;

namespace Core.Environment.OperatingSystemInfoImpl.Details.LinuxDetection
{
    public class LinuxDetailsResolver: IOperatingSystemDetailsResolver
    {
        private readonly Lazy<LinuxLinuxStandardBaseInfo> _lsbRelease = new Lazy<LinuxLinuxStandardBaseInfo>(() => new LinuxLinuxStandardBaseInfo());
        private readonly Lazy<string> _kernelInfo = new Lazy<string>(() => $"Kernel {new UnameWrapper().GetKernelRelease()}");
        public string ResolveVersion()
        {
            return _lsbRelease.Value.Release;
        }

        public string ResolveBuildVersion()
        {
            return _kernelInfo.Value;
        }

        public string ResolveName()
        {
            return _lsbRelease.Value.Id;
        }
    }
}