using System;
using Core.Extensions.ParserRelated;
using Core.Mathematics;
using Core.Mathematics.Impl;
using Core.Parser.Basic;

namespace Core.Parser.Special;

/// <summary>
/// Parses a GeoCircle from a string representation
/// </summary>
public class GeoCircleParser: IParser<IGeoCircle>
{
    /// <summary>
    /// The default initial Fallback Radius is set to 10 000.
    /// </summary>
    public const double InitialDefaultFallbackRadius = 10000;

    /// <summary>
    /// Default Fallback Radius for this Formatter
    /// </summary>
    public static double DefaultFallbackRadius = InitialDefaultFallbackRadius;

    private readonly IGeoFactory _factory;
    private readonly double _fallbackRadius;
    private readonly IParser<double> _doubleParser;

    /// <summary>
    /// Creates a new parser instance
    /// </summary>
    /// <param name="fallbackRadius">fallback radius</param>
    /// <param name="factory">used factory to create geoCircles. ignore or set this value to default or null to use the default factory.</param>
    /// <param name="doubleArrayParser">used parser for the input values. set default or null to use the default parser.</param>
    public GeoCircleParser(double? fallbackRadius = default, IGeoFactory? factory = default, IParser<double>? doubleArrayParser = default)
    {
        _fallbackRadius = fallbackRadius ?? DefaultFallbackRadius;
        _doubleParser = doubleArrayParser ?? new DoubleParser();
        _factory = factory ?? new GeoFactory();
    }

    public IGeoCircle Parse(IParserInput input)
    {
        var inputAsString = input.ReadAll();
        var values = _doubleParser.ParseToArrayOrEmpty(inputAsString);
        switch (values.Length)
        {
            case 2:
                return _factory.CreateCircle(values[0], values[1], _fallbackRadius);
            case 3:
                return _factory.CreateCircle(values[0], values[1], values[2]);
            default:
                throw new InvalidOperationException("expected two or 3 entries as input");
        }
    }
}
