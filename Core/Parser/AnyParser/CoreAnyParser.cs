using System;
using Core.Enums;
using Core.Mathematics;
using Core.Parser.Basic;
using Core.Parser.Special;

namespace Core.Parser.AnyParser;

/// <summary>
/// Core Implementation of the AnyParser Interface
/// </summary>
public class CoreAnyParser : IAnyParser
{
    public IParser<T> Create<T>()
    {
        if (typeof(T) == typeof(int)) return (IParser<T>)new IntegerParser();
        if (typeof(T) == typeof(bool)) return (IParser<T>)new BoolParser();
        if (typeof(T) == typeof(double)) return (IParser<T>)new DoubleParser();
        if (typeof(T) == typeof(DateTime)) return (IParser<T>)new DateTimeParser();
        if (typeof(T) == typeof(Guid)) return (IParser<T>) new GuidParser();
        if (typeof(T) == typeof(DatabaseType)) return (IParser<T>)new DatabaseTypeParser();
        if (typeof(T) == typeof(IGeoCircle)) return (IParser<T>)new GeoCircleParser();
        throw new InvalidOperationException($"{nameof(CoreAnyParser)} does not provide support for parsing type '{typeof(T)}'");
    }
}