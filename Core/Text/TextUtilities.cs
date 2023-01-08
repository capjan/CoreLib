using Core.Extensions.TextRelated;
using Core.Text.Impl;

namespace Core.Text;

/// <summary>
/// Util that provides the functions of the core classes in a convenient static matter
/// </summary>
public static class TextUtilities 
{
    /// <summary>
    /// Returns the Default Implementation of the Text Utilities
    /// </summary>
    /// <returns></returns>
    public static ITextUtilities Default() => new DefaultTextUtilities();

    /// <summary>
    /// Creates a random alphanumeric string that starts with a letter
    /// </summary>
    /// <param name="length">length of the string</param>
    /// <returns>A random string of the given length</returns>
    public static string CreateAlphanumericString(int length)
    {
        return Default()
            .Generators
            .RandomStrings
            .CreateAlphanumericString(length);
    }

    /// <summary>
    /// Creates a lorem ipsum text for placeholder purposes
    /// </summary>
    /// <param name="wordCount">count of words</param>
    /// <returns></returns>
    public static string CreateLoremIpsumText(int wordCount)
    {
        return Default()
            .Generators
            .LoremIpsum
            .CreateText(wordCount);
    }
}