# IRandomStringGenerator

Intended to generate alphanumeric strings.

```csharp
public interface IRandomStringGenerator
{
    string CreateAlphanumericString(int length);
}
```

## Notes
* The generated alphanumeric string always starts with a letter.

