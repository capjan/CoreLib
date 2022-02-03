namespace Core.Parser
{
    /// <summary>
    /// Provides a interface for parsing a string to a given output type.
    /// </summary>
    /// <typeparam name="T">Output Type</typeparam>
    /// <remarks>
    /// In fact a parser is often just a special case of an IConverter, where the input type is fixed to string.
    /// </remarks>
    public interface IParser<out T>
    {
        /// <summary>
        /// Parses the given input string to the given output type. Throws an exception if the parsing is not possible
        /// </summary>
        /// <param name="input">parser input that reads the input stream of text</param>
        /// <returns>The parsed object</returns>
        T Parse(IParserInput input);
    }
}
