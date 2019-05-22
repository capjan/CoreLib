# CLI Options with Attributes

Inspired by other attribute based command line argument parsers I added a attribute based frontend to the
already included [NDesk Options](./NDeskOptions.md) command line parser.

## Why?

This fronted is intended to make defining and parsing command line arguments as simple as possible.
With this approach the definition is done very quickly and the parsing is reduced to 3 lines of code.

Additionally the parser has already handled the --help and --version option with a default implementation.


## Example

1. Define a POCO class for the options
```csharp
public class Options : CliOptions
{
    [Option("r|recursive", "remove directories and their contents recursively")]
    public bool Recursive { get; set; }

    // The options h|help and v|version are inherited from CliOptions
}
```

2. Parse the options in your program.cs main method
```csharp
class Program
{

    static void Main(string[] args)
    {
        var parser = new OptionParser<Options>();
        if (!parser.TryParse(args, out var options))
            return;

        // done. options are parsed and valid. 
        // additionally --help and --version is also already handled.
    }
}
```
