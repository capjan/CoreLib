# Path Name Generator

**IPathNameGenerator** makes it easy to create unique path names inteded to be used as folder or file name.

## Interface
```csharp
string Generate(string rootDir);
```

This interface is short to make it easy to provide a custom implementation if it's needed.

Each implementation ensures that the generated name does not conflict with a file or folder name of the given `rootDir`.

## DefaultPathNameGenerator

The DefaultPathNameGenerator generates names with a customizable schema like:

`Prefix` `RandomPart` `Postfix`

With:
* `Prefix` defaults to ""
* `RandomPart` default to an alphanumeric string of 5 chars always starting with a letter
* `Postfix` defaults to ""



