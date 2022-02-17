namespace Core.Converters;

/// <summary>
/// Generic interface for a converter that converts a given input type into a given output type.
/// </summary>
/// <typeparam name="TInput">input type</typeparam>
/// <typeparam name="TOutput">output type</typeparam>
public interface IConverter<in TInput, out TOutput>
{
    /// <summary>
    /// Converts the given input type into the given output type.
    /// </summary>
    /// <param name="input">input value</param>
    /// <returns>The output value or throws an Exception if the conversion is not possible.</returns>
    TOutput Convert(TInput input);
}