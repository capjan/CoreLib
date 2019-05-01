# IUrlQueryBuilder

This interface makes it easy to create urls with parameters. The keys and values are url encoded.

## Interface
```csharp
public interface IUrlQueryBuilder
{
    IUrlQueryBuilder AddParameter(string key, string value);
    string           Build();
}
```

## Examples

### Basic usage
```csharp
var url = 
```

