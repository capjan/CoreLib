using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using Core.Parser;
using Core.Parser.Special;

namespace Core.Extensions.ParserRelated;

public static class ParserExtensions
{
    /// <summary>
    /// Parses the given input string to the given output Type. If the input can't be parsed the given fallback value is returned.
    /// </summary>
    /// <param name="parser">parser that performs the operation</param>
    /// <param name="input">input string that contains the string representation of the object</param>
    /// <param name="fallback">fallback value that is returned if any error occurs during parsing</param>
    /// <typeparam name="T">Type of the return value</typeparam>
    /// <returns>The parsed type or the given fallback value if an error cases</returns>
    public static T ParseOrFallback<T>(this IParser<T> parser, string input, T fallback)
    {
        try
        {
            return parser.Parse(input);
        }
        catch
        {
            return fallback;
        }
    }

    public static T? ParseOrNull<T>(this IParser<T> parser, string input) where T:struct
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return default;
        }
        return parser.Parse(input);
    }

    public static T Parse<T>(this IParser<T> parser, string input)
    {
        var parserInput = ParserInput.CreateFromString(input);
        return parser.Parse(parserInput);
    }

    public static T[] ParseToArrayOrFallback<T>(
        this IParser<T> parser,
        string input,
        T[] fallback,
        char separator = ',')
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("input is empty");
            return parser.ParseToArray(input, separator);
        }
        catch
        {
            return fallback;
        }
    }

    public static T[] ParseToArrayOrEmpty<T>(
        this IParser<T> parser,
        string input,
        char separator = ',')
    {
        return parser.ParseToArrayOrFallback(input, Array.Empty<T>(), separator);
    }

    public static T[]? ParseToArrayOrNull<T>(
        this IParser<T> parser,
        string input,
        string separator = ",")
    {
        if (string.IsNullOrWhiteSpace(input)) return default;
        try
        {
            // Here we must break the yielding to catch all exceptions.
            return parser.ParseToArray(input);
        }
        catch
        {
            return default;
        }
    }

    public static T[] ParseToArray<T>(this IParser<T> parser, string input, char separator = ',')
    {
        
        return input
            .Split(new []{separator}, StringSplitOptions.RemoveEmptyEntries)
            .Select(v => v.Trim())
            .Select(parser.Parse)
            .ToArray();
    }
    
    
    public static T? ParseOrDefault<T>(this IParser<T> parser, string input) 
    {
        try
        {
            return parser.Parse(input);
        }
        catch
        {
            return default;
        }
    }
}