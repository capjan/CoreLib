namespace Core.Diagnostics;

public interface ICliResult
{
    /// <summary>
    /// Called CLI Process (The wrapped program)
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    /// Used Arguments for the call
    /// </summary>
    string Arguments { get; set; }

    /// <summary>
    /// Returns the concatenated FileName + Arguments
    /// </summary>
    string CallSignature { get; }

    /// <summary>
    /// Exit Code
    /// </summary>
    int ExitCode { get; set; }

    /// <summary>
    /// Console Output (Merged stdout and stderr)
    /// </summary>
    string ConsoleOutput { get; set; }
}