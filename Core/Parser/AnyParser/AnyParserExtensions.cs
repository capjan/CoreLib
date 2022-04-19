using System.Collections.Generic;
using Core.Extensions.ParserRelated;

namespace Core.Parser.AnyParser;

/// <summary>
/// This extensions are providing the functionality by forwarding the concrete parser methods
/// </summary>
public static class AnyParserExtensions
{
    public static T Parse<T>(this IAnyParser parser, string input)
    {
        var concreteParser = parser.Create<T>();
        return concreteParser.Parse(input);
    }

    public static T ParseOrFallback<T>(this IAnyParser parser, string input, T fallback)
    {
        var concreteParser = parser.Create<T>();
        return concreteParser.ParseOrFallback(input, fallback);
    }

    public static T ParseOrDefault<T>(this IAnyParser parser, string input) where T: struct
    {
        var concreteParser = parser.Create<T>();
        return concreteParser.ParseOrDefault(input);
    }

    public static T[] ParseToArrayOrFallback<T>(this IAnyParser parser, string input, T[] fallback, char separator = ',')
    {
        var concreteParser = parser.Create<T>();
        return concreteParser.ParseToArrayOrFallback(input, fallback, separator);
    }

    public static T[] ParseToArrayOrEmpty<T>(this IAnyParser parser, string input, char separator = ',')
    {
        var concreteParser = parser.Create<T>();
        return concreteParser.ParseToArrayOrEmpty(input, separator);
    }

    public static T[] ParseToArray<T>(this IAnyParser parser, string input, char separator = ',')
    {
        var concreteParser = parser.Create<T>();
        return concreteParser.ParseToArray(input, separator);
    }
}