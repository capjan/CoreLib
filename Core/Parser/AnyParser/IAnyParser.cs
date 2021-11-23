namespace Core.Parser.AnyParser
{
    /// <summary>
    /// Interface of a non specific parser that can parse any supported types
    /// </summary>
    public interface IAnyParser
    {
        /// <summary>
        /// Creates a specific Parser for the given type or throws an exception if the type is not supported.
        /// </summary>
        /// <typeparam name="T">Output type of the parser</typeparam>
        /// <returns>A parser for the given type</returns>
        IParser<T> Create<T>();
    }
}
