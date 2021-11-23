using System;
using Core.Extensions.ParserRelated;
using Core.Mathematics;
using Core.Mathematics.Impl;
using Core.Parser.Basic;

namespace Core.Parser.Special
{
    /// <summary>
    /// Parses a GeoCircle from a string representation
    /// </summary>
    public class GeoCircleParser: IParser<IGeoCircle>
    {
        // todo: is it possible to define a good default value?
        public const double DefaultFallbackRadius = 10000;

        private readonly IGeoFactory _factory;
        private readonly double _fallbackRadius;
        private readonly IParser<double> _doubleParser;

        /// <summary>
        /// Creates a new parser instance
        /// </summary>
        /// <param name="fallbackRadius">fallback radius</param>
        /// <param name="factory">used factory to create geoCircles. ignore or set this value to default or null to use the default factory.</param>
        /// <param name="doubleArrayParser">used parser for the input values. set default or null to use the default parser.</param>
        public GeoCircleParser(double fallbackRadius = DefaultFallbackRadius, IGeoFactory? factory = default, IParser<double>? doubleArrayParser = default)
        {
            _fallbackRadius = fallbackRadius;
            _doubleParser = doubleArrayParser ?? new DoubleParser();
            _factory = factory ?? new GeoFactory();
        }

        public IGeoCircle Parse(string input)
        {
            var values = _doubleParser.ParseListToArrayOrEmpty(input);
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
}
