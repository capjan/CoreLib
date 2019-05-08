# CliRunner

CliRunner simplifies the creation of wrappers for console programs. 

The CLIRunner saves code in cases where you want to call a console 
program with a set of arguments and want to process the output.

## Interface
```csharp
public interface ICliRunner
{
    string Arguments { get; set; }
    string ReadToEnd();
    void   ReadLines(Action<string> callback);
}
```

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



