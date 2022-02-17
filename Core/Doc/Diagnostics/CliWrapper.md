# CliWrapper

Use the CliWrapper for I want to wrap a command line tool scenarios.

## Interface
```csharp
interface ICliWrapper {
   ICliResult Execute(string arguments);
}

public interface ICliResult
{
    // Returns the called CLI Process (the wrapped program)
    string FileName { get; }
    
    // Used Arguments for the call
    string Arguments { get; }
    
    // Returns the concatenated FileName + Arguments
    string CallSignature { get; }
    
    // Exit Code
    int ExitCode { get; }
    
    // Console Output (Merged stdout and stderr)
    string ConsoleOutput { get; }
 }
```

Example:

```cssharp
var ls = new CliWrapper("ls");
var result = ls.Execute("-al");
```
