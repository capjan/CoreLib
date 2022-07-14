using System.IO;

namespace Core.Parser;

public interface IParserInputPredicate
{
    /// <summary>
    /// Executes the given predicate and returns true on success otherwise nothing happens.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool IsMatch(IParserInput input, TextWriter writer);
}