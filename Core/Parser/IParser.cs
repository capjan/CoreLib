namespace Core.Parser
{
    /// <summary>
    /// Generic parser to parse a string to a given output type. 
    /// </summary>
    /// <typeparam name="T">Output Type</typeparam>
    public interface IParser<T>
    {
        /// <summary>
        /// Parses the given input string to the given output Type. If the input can't be parsed the given fallback value is returned.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        T ParseOrFallback(string input, T fallback);
    }
}
