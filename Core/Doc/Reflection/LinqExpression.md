# Linq Expression as Property Info Extractor

With Linq Expressions you can do many cool things.

I would like to point out a use case for which I like to use them. 
Namely for function parameters where you would normally need a magic string.

I saw the usage of this method for the first time in the fantastic 
library FluentValidation.

## Example

### Required classes
```csharp
public class Person
{
    public string Name { get; set; }
    public int    Age  { get; set; }
}

public class PropertyDumper<T>
{
    public string Dump<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var memberInfo = expression.GetMember();
        var name       = memberInfo.Name;
        var type       = typeof(TProperty);

        return $"{type.Name} {name}";
    }
}
```

### Usage
```csharp

var person = new Person { Name = "John Doe", Age  = 21 };
var dumper = new PropertyDumper<Person>();

Console.WriteLine(dumper.Dump(p=>p.Name));
Console.WriteLine(dumper.Dump(p=>p.Age));
```

### Output
```
String Name
Int32 Age
```