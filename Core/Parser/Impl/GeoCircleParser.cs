using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Mathematics;
using Core.Mathematics.Impl;

namespace Core.Parser.Impl
{
 /// <summary>
    /// Parses a GeoCircle from a string representation
    /// </summary>
    public class GeoCircleParser: IParser<IGeoCircle>
    {
        private readonly IGeoFactory _factory;
        private readonly double _fallbackRadius;
        private readonly IParser<double[]> _doubleArrayParser;

        /// <summary>
        /// Creates a new parser instance
        /// </summary>
        /// <param name="fallbackRadius">fallback radius</param>
        /// <param name="factory">used factory to create geoCircles. ignore or set this value to default or null to use the default factory.</param>
        /// <param name="doubleArrayParser">used parser for the input values. set default or null to use the default parser.</param>
        public GeoCircleParser(double fallbackRadius, IGeoFactory factory = default, IParser<double[]> doubleArrayParser = default)
        {
            _fallbackRadius = fallbackRadius;
            _doubleArrayParser = doubleArrayParser ?? new DoubleArrayParser();
            _factory = factory ?? new GeoFactory();
        }

        /// <summary>
        /// Returns the parsed value or the given fallback if parsing is not possible or failed.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public IGeoCircle ParseOrFallback(string input, IGeoCircle fallback)
        {
            try
            {
                var values = _doubleArrayParser.ParseOrFallback(input, null);
                if (values == null) return fallback;
                switch (values.Length)
                {
                    case 2:
                        return _factory.CreateCircle(values[0], values[1], _fallbackRadius);
                    case 3:
                        return _factory.CreateCircle(values[0], values[1], values[2]);
                    default:
                        return fallback;
                }
            }
            catch (Exception)
            {
                return fallback;
            }


        }
    }
}
