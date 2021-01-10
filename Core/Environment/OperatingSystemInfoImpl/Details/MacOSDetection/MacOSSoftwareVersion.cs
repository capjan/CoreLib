using Core.Diagnostics.Impl;

namespace Core.Environment.OperatingSystemInfoImpl.Details.MacOSDetection
{
    /// <summary>
    /// Wrapper for the sw_ver cli tool to determine information about the OS on macOS
    /// </summary>
    internal class MacOSSoftwareVersion
    {
        public        string ProductName    { get; }
        public        string ProductVersion { get; }
        public        string BuildVersion   { get; }
        private const string CliProgramName = "sw_vers";

        public MacOSSoftwareVersion()
        {
            var cli                  = new CliRunner(CliProgramName);
            var detectedVersion      = string.Empty;
            var detectedBuildVersion = string.Empty;
            var detectedProductName  = string.Empty;
                
            cli.ReadLines(line =>
            {
                if (line == null) return;
                if (line.StartsWith("ProductVersion:"))
                    detectedVersion = line.Substring(15).Trim();
                else if (line.StartsWith("BuildVersion:"))
                    detectedBuildVersion = line.Substring(14).Trim();
                else if (line.StartsWith("ProductName:"))
                    detectedProductName = line.Substring(12).Trim();
            });

            ProductName    = detectedProductName;
            ProductVersion = detectedVersion;
            BuildVersion   = detectedBuildVersion;
        }
    }
}