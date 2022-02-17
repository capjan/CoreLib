using System;
using System.Text.RegularExpressions;
using Core.Diagnostics.Impl;
using Core.Extensions.ParserRelated;
using Core.Parser.Basic;

namespace Core.Environment.OperatingSystemInfoImpl.Details.WindowsDetection;

/// <summary>
/// Wrapper for the embedded CLI.exe command ver
/// </summary>
internal static class WindowsVerWrapper
{
    public static Version GetVersion()
    {
        var cli    = new CliRunner("cmd.exe", "/C ver");
        var output = cli.ReadToEnd();


        const string pattern = @"(?<major>\d+)(\.(?<minor>\d+)(\.(?<build>\d+))?)?";
        var          m       = Regex.Match(output, pattern);
        var major = 0;
        var minor = 0;
        var build = 0;

        if (m.Success)
        {
            var parser = new IntegerParser();
            major  = parser.ParseOrFallback(m.Groups["major"].Value, 1);
            minor  = parser.ParseOrFallback(m.Groups["minor"].Value, 0);
            build  = parser.ParseOrFallback(m.Groups["build"].Value, 0);
        }
        return new Version(major, minor, build);
    }
}