using System;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using Core.Reflection;

namespace Core.Extensions.ReflectionRelated;

public static class AssemblyExt
{
    public static bool TryGetAttribute<T>(this Assembly assembly, out T? attribute) where T : Attribute
    {
        try
        {
            attribute = assembly.GetCustomAttribute<T>();
            return attribute != null;
        }
        catch (Exception)
        {
            attribute = null;
            return false;
        }


    }

    public static Lazy<TResult> GetAttributeResultLazy<TAttribute, TResult>(
        this Assembly assembly, Func<TAttribute, TResult> callback) where TAttribute : Attribute
    {

        return new Lazy<TResult>(() =>
        {
            if (assembly.TryGetAttribute(out TAttribute? attribute))
            {
                return callback(attribute!);
            }
            throw new InvalidCastException($"expected success of TryGetAttribute for {nameof(TAttribute)}");
        });
    }

    public static Lazy<string> GetAttributeResultLazy<TAttribute>(
        this Assembly assembly, Func<TAttribute, string> callback) where TAttribute : Attribute
    {
        return GetAttributeResultLazy<TAttribute, string>(assembly, callback);
    }

    public static string GetBestMatchingVersion(this IAssemblyInfo assemblyInfo)
    {
        if (!string.IsNullOrWhiteSpace(assemblyInfo.Version)) return assemblyInfo.Version;
        if (!string.IsNullOrWhiteSpace(assemblyInfo.AssemblyVersion)) return assemblyInfo.AssemblyVersion;
        if (!string.IsNullOrWhiteSpace(assemblyInfo.FileVersion)) return assemblyInfo.FileVersion;
        return "1.0";
    }

    public static string GetVersionSummary(this IAssemblyInfo info)
    {
        return $"Version {info.GetBestMatchingVersion()}";
    }

    /// <summary>
    /// Returns the absolute path of the folder where the assembly is stored in the file system.
    /// </summary>
    public static string GetFolderPath(this Assembly assembly)
    {
        var assemblyFilePath  = assembly.Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyFilePath);
        return assemblyDirectory ?? "";
    }
}