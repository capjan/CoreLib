# IUrlBuilder

This interface makes it easy to create valid internet urls. create urls with parameters. The keys and values are url encoded.

## Interface
```csharp
public interface IUrlQueryBuilder
{
    IUrlBuilder Credentials(string userName, string password);
    IUrlBuilder Port(int? value);
    IUrlBuilder AddPath(params string[] paths);
    IUrlBuilder AddParam(string key, string value);
    
    IUrlBuilder ClearPath();
    IUrlBuilder ClearParameter();
    
    string      Build();
}
```

## Examples

### Url with Query Parameters
```csharp
var url = new DefaultUrlQueryBuilder("https://www.test.com")
          .AddParam("name", "John Doe")
          .AddParam("mail", "john.doe@domain.com")
          .Build();

// url = "https://www.test.com?name=John+Doe&mail=john.doe%40domain.com"
```

