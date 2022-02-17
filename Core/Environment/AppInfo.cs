using System;
using System.Reflection;
using Core.Extensions.ReflectionRelated;

namespace Core.Environment;

public class AppInfo
{
    public string ApplicationFolder { get; }

    public AppInfo()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null) throw new InvalidOperationException("entry assembly must not be null");
        ApplicationFolder = entryAssembly.GetFolderPath();
    }
}