# NDesk Options

NDesk Options makes it easy to parse command line arguments.

Take a look at [http://www.ndesk.org/Options](http://www.ndesk.org/Options) for further documentation.

Please note: CoreLib provides a [attribute based fronted](./AttributeBasedOptions.md) for NDesk.

## Example

```csharp
var showVersion = false;
var outputFolder = "";

var options = new OptionSet
{
    {"v|version", "Show version information", v => showVersion = v != null},
    {"o|output=", "Set output folder to {PATH}", path => outputFolder = path}
};

// get formatted description as string
var description = options.GetOptionDescriptions();

// write description directly to std out
options.WriteOptionDescriptions(Console.Out);

try
{
    // extra contains the remaining 'non option' arguments
    var extra = options.Parse(new[] {"-v", "--output", "C:\\temp"});
}
catch (Exception e)
{
    Console.Error.WriteLine("parse error.");
    options.WriteOptionDescriptions(Console.Out);
    throw;
}
```