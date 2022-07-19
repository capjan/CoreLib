using System.IO;

namespace Core.Parser;

/// <summary>
/// Predicate that can be used to check if the parser input is matching it. Typically it is build with a predicate builder.
/// </summary>
public interface IParserInputPredicate
{
    /// <summary>
    /// Executes the given predicate and returns true on success otherwise nothing happens.
    /// </summary>
    /// <param name="input">Input from which characters are read</param>
    /// <param name="writer">output to be written if the predicate is a match</param>
    /// <returns>true, if the predicate is a match, otherwise false</returns>
    bool IsMatch(IParserInput input, TextWriter writer);
}