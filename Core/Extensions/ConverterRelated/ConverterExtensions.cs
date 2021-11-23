using Core.Converters;
using Core.Text.Impl;

namespace Core.Extensions.ConverterRelated
{
    public static class ConverterExtensions
    {
        /// <summary>
        /// Converts the input value to the expected output with fallback value for any error cases.
        /// </summary>
        /// <param name="converter">used converter of the operation</param>
        /// <param name="input">input value</param>
        /// <param name="fallback">fallback value of any error cases</param>
        /// <typeparam name="TInput">Type of the input</typeparam>
        /// <typeparam name="TOutput">Type of the output</typeparam>
        /// <returns>The converted output value or the fallback value</returns>
        public static TOutput ConvertOrFallback<TInput, TOutput>(this IConverter<TInput, TOutput> converter, TInput input, TOutput fallback)
        {
            try
            {
                return converter.Convert(input);
            }
            catch
            {
                return fallback;
            }
        }
    }
}
