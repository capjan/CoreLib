using System;
using Core.Enums;
using Core.Text;

namespace Core.Parser;

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
    /// Returns the last character that is peeked or (if the lookahead is 0) the last read character. Throws an Exception if called on offset 0 
    /// </summary>
    /// <remarks>This is required to implement the word boundary predicate</remarks>
    char LastCharacter { get; }
    
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
    /// Count of chars in the lookahead
    /// </summary>
    int LookaheadCount { get; set; }

    /// <summary>
    /// Clears the current Lookahead and resets the reading position to the last read text position
    /// </summary>
    void ClearLookahead();

    /// <summary>
    /// Forces the input to read the complete lookahead
    /// </summary>
    void ReadLookahead();
}