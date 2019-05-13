# Attribute Based Option Fronted for the NDesk Options Parser

Inspired by attribute based command line argument parsers I added a attribute based frontend to the
included [NDesk Options](http://www.ndesk.org/Options) command line parser.

## Why?

This fronted is intended to make defining and parsing command line arguments as simple as possible.
With this approach the definition is done very quickly and the parsing is reduced to 3 lines of code.

Additionally the parser has already handled the --help and --version option with a default implementation.


## Example

1. Define a POCO Class for the Options
```csharp
public class Options : CliOptions
{
    [Option("r|recursive", "remove directories and their contents recursively")]
    public bool Recursive { get; set; }

    // please h|help and v|version is added by inheriting from CliOptions
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