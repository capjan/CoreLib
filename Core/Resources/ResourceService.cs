using System;
using System.IO;
using System.Reflection;

namespace Core.Resources;

/// <summary>
/// Service that makes it easy to work with embedded resources
/// </summary>
public class ResourceService
{
    private readonly Assembly _assembly;

    public ResourceService(Assembly assembly)
    {
        _assembly = assembly;
    }

    public static ResourceService FromType(Type type)
    {
        var asm = type.Assembly;
        return new ResourceService(asm);
    }

    /// <summary>
    /// Returns the embedded resource by it's name. Hint: Call GetNames to determine all existing names.
    /// </summary>
    /// <param name="name">name of the embedded resource (namespace of the assembly and full Path)</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">thrown if the resource for the name does not exists</exception>
    public Stream GetStreamByName(string name)
    {
        return _assembly.GetManifestResourceStream(name) ?? throw new InvalidOperationException($"failed to get a stream for the name \"{name}\"");
    }

    /// <summary>
    /// Returns all resource names in the assembly
    /// </summary>
    /// <returns></returns>
    public string[] GetNames()
    {
        return _assembly.GetManifestResourceNames();
    }
}
