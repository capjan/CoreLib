namespace Core.Diagnostics.Impl;

public class CliResult : ICliResult
{
    /// <summary>
    /// Called CLI Process (The wrapped program)
    /// </summary>
    public string FileName { get; set; } = "";

    /// <summary>
    /// Used Arguments for the call
    /// </summary>
    public string Arguments { get; set; } = "";

    /// <summary>
    /// Returns the concatenated FileName + Arguments
    /// </summary>
    public string CallSignature => $"{FileName} {Arguments}";

    /// <summary>
    /// Exit Code
    /// </summary>
    public int ExitCode { get; set; }

    /// <summary>
    /// Console Output (Merged stdout and stderr)
    /// </summary>
    public string ConsoleOutput { get; set; } = "";
}