# ILoremIpsumGenerator

makes it easy to create random text for placeholder/layout purposes

## Example
```csharp
var gen = new LoremIpsumGenerator();

// creates a text of 100 word length
var text = gen.CreateText(100);

// writes 100 words intro the given file
using (var writer = new StreamWriter("C:\tmp\testfile.txt"))
    gen.WriteText(100, writer);
```

## Notes
* The first 5 words are always the same by design. `lorem ipsum dolor sit amet`

