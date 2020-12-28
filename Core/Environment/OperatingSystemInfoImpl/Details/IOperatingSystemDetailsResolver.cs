namespace Core.Environment.OperatingSystemInfoImpl.Details
{
    /// <summary>
    /// Provides possibilities to resolve common information about an operating system.
    /// </summary>
    public interface IOperatingSystemDetailsResolver
    {
        string ResolveVersion();
        string ResolveBuildVersion();
        string ResolveName();
    }
}