# AssemblyInfo

AssemblyInfo is intended to make it easy to access the given assembly attributes in code.

## Exposed Properties
* Title
* Description
* Product
* Version - used for NuGet
* AssemblyVersion
* FileVersion
* Company
* Copyright
* Trademark

Note: *All properties are exposed as string*

## Example

```csharp
var asmInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());

// access the assembly informations via properties
Console.WriteLine(asmInfo.Title);
Console.WriteLine(asmInfo.Description);
Console.WriteLine(asmInfo.Product);
Console.WriteLine(asmInfo.Version);
Console.WriteLine(asmInfo.AssemblyVersion);
Console.WriteLine(asmInfo.FileVersion);
Console.WriteLine(asmInfo.Company);
Console.WriteLine(asmInfo.Copyright);
Console.WriteLine(asmInfo.Trademark);
```