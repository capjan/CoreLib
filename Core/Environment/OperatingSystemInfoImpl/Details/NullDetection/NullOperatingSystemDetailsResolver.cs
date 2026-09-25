namespace Core.Environment.OperatingSystemInfoImpl.Details.NullDetection;

internal sealed class NullOperatingSystemDetailsResolver: IOperatingSystemDetailsResolver
{
    public string ResolveVersion()
    {
        return string.Empty;
    }

    public string ResolveBuildVersion()
    {
        return string.Empty;
    }

    public string ResolveName()
    {
        return string.Empty;
    }
}