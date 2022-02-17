using System;
using System.IO;
using System.Numerics;
using Core.Generic;

namespace Core.Text.Formatter;

/// <summary>
/// Formatter for generic types that are numbers. Supports integer and floating point numbers.
/// </summary>
/// <typeparam name="T">must be an integer, floating point, decimal or bigint type</typeparam>
public class GenericNumberFormatter<T> : IFormattableTextFormatter<T>
{
    public GenericNumberFormatter()
    {
        var typeChecker = TypeChecker<T>.Numeric();
        if (!typeChecker.IsValid())
            throw new ArgumentException("the given generic type is not a number type");
    }

    public string Format { get; set; } = "g";
    public IFormatProvider? FormatProvider { get; set; }

    public void Write(T value, TextWriter writer)
    {
        writer.Write(FormatValue(value));
    }

    private string FormatValue(T value)
    {
        switch (value) {
            case byte byteValue: return byteValue.ToString(Format, FormatProvider);
            case sbyte sbyteValue: return sbyteValue.ToString(Format, FormatProvider);
            case short shortValue: return shortValue.ToString(Format, FormatProvider);
            case ushort ushortValue: return ushortValue.ToString(Format, FormatProvider);
            case int intValue: return intValue.ToString(Format, FormatProvider);
            case uint uintValue: return uintValue.ToString(Format, FormatProvider);
            case long longValue: return longValue.ToString(Format, FormatProvider);
            case ulong ulongValue: return ulongValue.ToString(Format, FormatProvider);
            case float floatValue: return floatValue.ToString(Format, FormatProvider);
            case double doubleValue: return doubleValue.ToString(Format, FormatProvider);
            case decimal decimalValue: return decimalValue.ToString(Format, FormatProvider);
            case BigInteger bigIntValue: return bigIntValue.ToString(Format, FormatProvider);
            default: throw new ArgumentException($"type {typeof(T).Name} is not a supported number type.");
        }
    }

}