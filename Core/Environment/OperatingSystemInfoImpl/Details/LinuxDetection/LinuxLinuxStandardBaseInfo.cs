using System;
using Core.Diagnostics.Impl;

namespace Core.Environment.OperatingSystemInfoImpl.Details.LinuxDetection;

internal sealed class LinuxLinuxStandardBaseInfo
{
    /// <summary>
    /// Id of the running linux distribution. e.g. Ubuntu
    /// </summary>
    public string Id { get; }
    /// <summary>
    /// Single line of text description. e.g. Ubuntu 20.04.1 LTS
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// Release number of the distribution. e.g. 20.04
    /// </summary>
    public string Release { get; }
    /// <summary>
    /// Codename according to the distribution release. e.g. focal
    /// </summary>
    public string CodeName { get; }

    public LinuxLinuxStandardBaseInfo()
    {
        var cli = new CliRunner("lsb_release", "--all");
        var detectedId = "";
        var detectedDescription = "";
        var detectedRelease = "";
        var detectedCodeName = "";
        cli.ReadLines(line =>
        {
            if (line.StartsWith("Distributor ID:\t", StringComparison.Ordinal)) detectedId = line.Substring(16);
            if (line.StartsWith("Description:\t", StringComparison.Ordinal)) detectedDescription = line.Substring(13);
            if (line.StartsWith("Release:\t", StringComparison.Ordinal)) detectedRelease = line.Substring(9);
            if (line.StartsWith("Codename:\t", StringComparison.Ordinal)) detectedCodeName = line.Substring(10);
        });
        Id = detectedId;
        Description = detectedDescription;
        Release = detectedRelease;
        CodeName = detectedCodeName;
    }
        
}