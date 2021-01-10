# OperatingSystemInfo

This class collects information about the operating system and provides this information in a very easy way.

**Example:**

```C#
var info = new OperatingSystemInfo();

Console.WriteLine(info);
Console.WriteLine(info.Name);
Console.WriteLine(info.Version);
Console.WriteLine(info.Platform);
Console.WriteLine(info.Build);

// info.Platform contains an enumeration of Windows, OSX, Linux, Unknown
```

