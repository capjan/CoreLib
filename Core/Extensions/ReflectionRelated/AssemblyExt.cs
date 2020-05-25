using System;
using System.IO;
using System.Reflection;
using Core.Reflection;

namespace Core.Extensions.ReflectionRelated
{
    public static class AssemblyExt
    {
        public static bool TryGetAttribute<T>(this Assembly assembly, out T attribute) where T : Attribute
        {
            attribute = assembly.GetCustomAttribute<T>();
            return attribute != null;
        }

        public static Lazy<TResult> GetAttributeResultLazy<TAttribute, TResult>(
            this Assembly assembly, Func<TAttribute, TResult> callback) where TAttribute : Attribute
        {
            return new Lazy<TResult>(() => assembly.TryGetAttribute(out TAttribute a) ? callback(a) : default);
        }

        public static Lazy<string> GetAttributeResultLazy<TAttribute>(
            this Assembly assembly, Func<TAttribute, string> callback) where TAttribute : Attribute
        {
            return new Lazy<string>(() => assembly.TryGetAttribute(out TAttribute a) ? callback(a) : default);
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
            return assemblyDirectory;
        }
    }
}
