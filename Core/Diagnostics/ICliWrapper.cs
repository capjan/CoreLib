namespace Core.Diagnostics;

/// <summary>
/// Interface for a component that acts as a wrapper for a command line program
/// </summary>
public interface ICliWrapper
{
    /// <summary>
    /// Executes the wrapped command line program with the given arguments
    /// </summary>
    /// <param name="arguments">arguments that are passed to the command line tool</param>
    /// <returns>The Result of executing the command line tool</returns>
    ICliResult Execute(string arguments);
}