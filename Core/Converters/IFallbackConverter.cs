namespace Core.Converters
{
    /// <summary>
    /// Generic interface for a converter that transforms a given input type into the given output type. If a conversion is not possible it returns a given fallback. 
    /// </summary>
    /// <typeparam name="TInput">Input Type</typeparam>
    /// <typeparam name="TOutput">Output Type</typeparam>
    public interface IFallbackConverter<in TInput, TOutput>
    {
        /// <summary>
        /// Converts the given input value into the given output. If the conversion fails the given fallback value is returned.
        /// </summary>
        /// <param name="input">Input Value</param>
        /// <param name="fallback">Fallback Value that is returned if the conversion fails.</param>
        /// <returns>Returns the converted value on success or the given fallback value on failure.</returns>
        TOutput Convert(TInput input, TOutput fallback);
    }
}