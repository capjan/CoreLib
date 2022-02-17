namespace Core.Diagnostics;

public interface ICliResult
{
    /// <summary>
    /// Called CLI Process (The wrapped program)
    /// </summary>
    string FileName { get; init; }

    /// <summary>
    /// Used Arguments for the call
    /// </summary>
    string Arguments { get; init; }

    /// <summary>
    /// Returns the concatenated FileName + Arguments
    /// </summary>
    string CallSignature { get; }

    /// <summary>
    /// Exit Code
    /// </summary>
    int ExitCode { get; init; }

    /// <summary>
    /// Console Output (Merged stdout and stderr)
    /// </summary>
    string ConsoleOutput { get; init; }
}