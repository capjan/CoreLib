namespace Core.Parser.Basic.Interfaces
{
    public interface IAnyParser
    {
        /// <summary>
        /// Parses a given input string to a specific output type T.
        /// </summary>
        /// <param name="input">the string value to parse</param>
        /// <param name="fallback">fallback, if the input cannot be parsed</param>
        /// <typeparam name="T">currently supported types: int, int?, double, double?, DateTime, DateTime?, bool, bool?</typeparam>
        /// <returns></returns>
        T ParseOrFallback<T>(string input, T fallback = default);
    }
}