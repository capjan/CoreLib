using System.IO;

namespace Core.Resources;

/// <summary>
/// Extenstion Methods for ResourceService
/// </summary>
public static class ResourceServiceRelated
{
    /// <summary>
    /// Gets the given Resource (by name) as string
    /// </summary>
    /// <param name="res">the resource service</param>
    /// <param name="name">name of the embedded resource</param>
    /// <returns>The resource as string</returns>
    public static string GetStringByName(this ResourceService res, string name)
    {
        using var stream = res.GetStreamByName(name);
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }
}