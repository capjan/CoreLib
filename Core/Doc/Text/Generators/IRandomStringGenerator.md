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
* This generator uses the regular-purpose random source and is not suitable for passwords, tokens, or other secrets. Use `System.Security.Cryptography.RandomNumberGenerator` for security-sensitive values.
