# MemberInfo via Linq Expression

With Linq Expressions you can do many cool things. Here I would like to point
out a use case for which I find them incredibly useful.

If you write an API that should apply something to the properties of a certain
type. And if the property is to be determined by developers in the code, then
this method is incredibly convenient.

Especially because the selection of properties is directly limited to the 
given type and this method allows the auto-completion of Intellisense.

## Advantages
* No magic strings
* No nameof instruction
* Code assistant (Intellisense) provides optimal support during input.



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
        
        // Here you can additional check the Type Property of memberInfo 
        // to cast it to a more specific info class like PropertyInfo, etc.

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