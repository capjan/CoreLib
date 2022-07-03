using System;
using System.IO;
using System.Text;
using Core.Parser;

namespace Core.Extensions.ParserRelated;

public static class ParserInputExtensions
{
    /// <summary>
    /// Reads all remaining input chars as string.
    /// </summary>
    /// <param name="input">Input</param>
    /// <returns>All remaining chars of the input.</returns>
    public static string ReadAll(this IParserInput input)
    {
        var sb = new StringBuilder();
        while (input.TryReadChar(out var readChar))
        {
            sb.Append(readChar);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Peeks the next character from the input and fires the given predicate against the peeked character if the peek was successful. 
    /// </summary>
    /// <param name="input">input of the operation</param>
    /// <param name="predicate">predicate that must succeed</param>
    /// <param name="peekedInput">The peeked character if the predicate was successful</param>
    /// <returns>true if the peek and predicate operation was successful, otherwise false and nothing happens to the input</returns>
    public static bool TryPeekMatch(this IParserInput input, IParserInputPredicate predicate, out string peekedInput)
    {
        var oldLookahead = input.LookaheadCount;
        peekedInput = string.Empty;
        
        var sb = new StringBuilder();
        using var sw = new StringWriter(sb);
        var isPredicateSuccessful = predicate.IsMatch(input, sw);
        if (isPredicateSuccessful)
        {
            peekedInput = sb.ToString();
            return true;
        }
        input.LookaheadCount = oldLookahead;
        return false;
    }
    
    /// <summary>
    /// Peeks the next character from the input and fires the given predicate against the peeked character if the peek was successful. 
    /// </summary>
    /// <param name="input">input of the operation</param>
    /// <param name="predicate">predicate that must succeed</param>
    /// <param name="peekedCharacter">The peeked character if the predicate was successful</param>
    /// <returns>true if the peek and predicate operation was successful, otherwise false and nothing happens to the input</returns>
    public static bool TryPeekMatch(this IParserInput input, Func<char, bool> predicate, out char peekedCharacter)
    {
        var oldLookahead = input.LookaheadCount;
        var isPeekSuccessful = input.TryPeekChar(out peekedCharacter);
        if (!isPeekSuccessful)
        {
            input.LookaheadCount = oldLookahead;
            return false;
        }
        var isMatchSuccessful = predicate(peekedCharacter);
        if (isMatchSuccessful) return isMatchSuccessful;
        input.LookaheadCount = oldLookahead;
        return isMatchSuccessful;
    }

    /// <summary>
    /// reads the next character from the input that match the given predicate or does nothing. 
    /// </summary>
    /// <param name="input">input of the operation</param>
    /// <param name="predicate">predicate that must succeed</param>
    /// <param name="readMatch">The peeked character if the predicate was successful</param>
    /// <returns>true if the peek and predicate operation was successful, otherwise false and nothing happens to the input</returns>
    public static bool TryReadMatch(this IParserInput input, IParserInputPredicate predicate, out string readMatch)
    {
        var oldLookahead = input.LookaheadCount;
        readMatch = string.Empty;
        
        var sb = new StringBuilder();
        using var sw = new StringWriter(sb);
        var isPredicateSuccessful = predicate.IsMatch(input, sw);
        if (isPredicateSuccessful)
        {
            readMatch = sb.ToString();
            input.ReadLookahead();
            return true;
        }
        input.LookaheadCount = oldLookahead;
        return false;
    }
    
    /// <summary>
    /// returns true, if the peek was successful and the peeked character equals the given matchChar. If the peek or the given character does not match nothing happens (no position update, no lookahead change, etc).
    /// </summary>
    /// <param name="input">input that contains the input of the parser</param>
    /// <param name="matchCharacter">given character that must be equal to the peeked character</param>
    /// <returns>true, if the peek was successful and the peeked character is matching the given character</returns>
    public static bool TryPeekMatch(this IParserInput input, char matchCharacter)
    {
        return TryPeekMatch(input, ch => ch == matchCharacter, out _);
    }

    /// <summary>
    /// Tries to read the next character that matches the given predicate, otherwise nothing happens.
    /// </summary>
    /// <param name="input">input of the operation</param>
    /// <param name="predicate">predicate that must succeed to proceed the read.</param>
    /// <param name="readCharacter">returns the read character</param>
    /// <returns>true, if the read and the predicate was successful, otherwise false and nothing happens to the input.</returns>
    public static bool TryReadMatch(this IParserInput input, Func<char, bool> predicate, out char readCharacter)
    {
        var oldLookahead = input.LookaheadCount;
        input.LookaheadCount = 0;
        if (input.TryPeekMatch(predicate, out readCharacter))
        {
            input.ReadLookahead();
            return true;
        }

        input.LookaheadCount = oldLookahead;
        return false;
    }

    /// <summary>
    /// Tries to read the next character from input if the character matches the given one, otherwise nothing happens.
    /// </summary>
    /// <param name="input">input of the operation</param>
    /// <param name="matchCharacter">matching character that must match the read character for an successful result.</param>
    /// <returns>true, if the read was successful and the read character matches the given one, otherwise nothing happens.</returns>
    public static bool TryReadMatch(this IParserInput input, char matchCharacter)
    {
        return TryReadMatch(input, readCharacter => readCharacter == matchCharacter, out _);
    }
}