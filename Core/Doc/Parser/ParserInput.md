# Parser Input

[Home](../README.md) / [Parser](./README.md)

The Parser Input provides an abstraction of a stream based text input with the possibility to write predicate based matches. 

That makes it much easier to write a parser by hand.

## Features
* wraps a TextReader stream as input source, so it's a very flexible input that welcomes large files.
* multiple character lookahead. (implemented by internal cache - streams don't need to be seekable)
* Provides offset (char index) and text position (line and column number)

## Interface

```csharp
public interface IParserInput : IDisposable
{
    /// <summary>
    /// Returns the Current Offset (char index) in the input. Offsets are 0 (zero) based.
    /// </summary>
    int Offset { get; }

    /// <summary>
    /// Returns the current text position (line and column number) in the input. line and column numbers are dedicated to humans, so they start with 1 (one).
    /// </summary>
    ITextPosition TextPosition { get; }

    /// <summary>
    /// Look ahead (in the future) what the next potentially read character is without reading it. A call to this function doesn't change any position information.
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    bool TryPeekChar(out char ch);

    /// <summary>
    /// Reads the next potentially character and updates the position and offset.
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    bool TryReadChar(out char ch);

    /// <summary>
    /// Counts of the current lookahead
    /// </summary>
    int LookaheadCount { get; }

    /// <summary>
    /// Clears the current lookup
    /// </summary>
    void ClearLookahead();

    /// <summary>
    /// forces the input to read the complete lookahead
    /// </summary>
    void ReadLookahead();
}
```
## Example
```csharp
const string source = "abcdef 12345";
using var input = ParserInput.CreateFromString(source);
// use the input
```
