# CliRunner

## Example

### Classic Approach

```csharp
var psi = new ProcessStartInfo(filePath)
{
    Arguments              = arguments,
    UseShellExecute        = false,
    RedirectStandardOutput = true,
    RedirectStandardError  = true,
    CreateNoWindow         = false
};

var stdout = "";
using (var p = Process.Start(psi))
{                                
    stdout = p.StandardOutput.ReadToEnd();
    p.WaitForExit();                                
}

// do something with stdout
```

### CliRunner Approach
```csharp
var stdout = new CliRunner(msbuildPath, "/version").ReadToEnd();
// do something with stdout
```



