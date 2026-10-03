using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        using var p = Process.Start(psi)
            ?? throw new InvalidOperationException($"Failed to start process '{_fileName}'.");

        var stdOutTask = p.StandardOutput.ReadToEndAsync();
        var stdErrTask = p.StandardError.ReadToEndAsync();
        if (!p.WaitForExit(_globalTimeoutInMilliseconds))
        {
            p.Kill();
            p.WaitForExit();
            Task.WhenAll(stdOutTask, stdErrTask).GetAwaiter().GetResult();
            throw new TimeoutException($"Process '{_fileName}' exceeded the timeout of {_globalTimeoutInMilliseconds} milliseconds.");
        }

        var output = Task.WhenAll(stdOutTask, stdErrTask).GetAwaiter().GetResult();
        var mergedOutput = output[0] + output[1];
        return new CliResult
        {
            FileName = psi.FileName,
            Arguments = psi.Arguments,
            ExitCode = p.ExitCode,
            ConsoleOutput = mergedOutput
        };
    }
}
