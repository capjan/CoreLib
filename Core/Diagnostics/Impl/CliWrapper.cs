using System.Diagnostics;

namespace Core.Diagnostics.Impl;

/// <summary>
/// Default implementation for a ICliWrapper
/// </summary>
public class CliWrapper : ICliWrapper
{
    private readonly string _fileName;
    private readonly string? _workingDirectory;
    private readonly int _globalTimeoutInMilliseconds;

    public CliWrapper(string fileName, string? workingDirectory = null, int timeout = 20000)
    {
        _fileName = fileName;
        _workingDirectory = workingDirectory;
        _globalTimeoutInMilliseconds = timeout;
    }

    public ICliResult Execute(string arguments)
    {
        var psi = new ProcessStartInfo
        {
            FileName = _fileName,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        if (_workingDirectory != null)
        {
            psi.WorkingDirectory = _workingDirectory;
        }

        using var p = Process.Start(psi)!;
        p.WaitForExit(_globalTimeoutInMilliseconds);
        var mergedOutput = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
        return new CliResult
        {
            FileName = psi.FileName,
            Arguments = psi.Arguments,
            ExitCode = p.ExitCode,
            ConsoleOutput = mergedOutput
        };
    }
}