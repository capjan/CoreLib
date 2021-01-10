using Core.Diagnostics.Impl;

namespace Core.Environment.OperatingSystemInfoImpl.Details.LinuxDetection
{
    public class UnameWrapper
    {
        /// <summary>
        /// Returns the Name of the kernel. Eg. Linux or Darwin
        /// </summary>
        /// <returns></returns>
        public string GetKernelName() => Uname("-s");
        
        /// <summary>
        /// Returns the Release of the kernel: e.g. 4.19.128
        /// </summary>
        /// <returns></returns>
        public string GetKernelRelease() => Uname("-r");
        
        /// <summary>
        /// Returns Version information about the kernel. e.g. build-date, etc
        /// </summary>
        /// <returns></returns>
        public string GetKernelVersion() => Uname("--kernel-version");
        
        private string Uname(string args)
        {
            return new CliRunner("uname", args).ReadToEnd();
        }
    }
}