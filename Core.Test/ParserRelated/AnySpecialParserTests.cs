using System;
using System.Linq;
using System.Numerics;
using Core.Enums;
using Core.Mathematics;
using Core.Parser;
using Core.Parser.Special;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class AnySpecialParserTests
    {
        [Fact]
        public void ParseOrFallback_IntArray()
        {
            const string stringValue = "1, 2";
            var sut = CreateSut();
            var intArrayParser = new IntArrayParser();

            var expectedResult = intArrayParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<int[]>(stringValue);

            Assert.True(result.All(x => expectedResult.Contains(x)));

            var fallback = default(int[]);
            result = sut.ParseOrFallback<int[]>("random");
            Assert.Equal(fallback, result);
        }

        [Fact]
        public void ParseOrFallback_DoubleArray()
        {
            const string stringValue = "1.123, 2.321";
            var sut = CreateSut();
            var doubleArrayParser = new DoubleArrayParser();

            var expectedResult = doubleArrayParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<double[]>(stringValue);

            Assert.True(result.All(x => expectedResult.Contains(x)));

            var fallback = default(double[]);
            result = sut.ParseOrFallback<double[]>("random");
            Assert.Equal(fallback, result);
        }

        [Fact]
        public void ParseOrFallback_DatabaseType()
        {
            const string stringValue = "SQLServer";
            var sut = CreateSut();
            var databaseTypeParser = new DatabaseTypeParser();

            var expectedResult = databaseTypeParser.ParseOrFallback(stringValue, default);
            var result = sut.ParseOrFallback<DatabaseType>(stringValue);

            Assert.Equal(expectedResult, result);

            var fallback = default(DatabaseType);
            result = sut.ParseOrFallback<DatabaseType>("random");
            Assert.Equal(fallback, result);
        }
        [Fact]
        public void ParseOrFallback_GeoCircle()
        {
            const string stringValue = "54.396034, 10.179827, 5000";
            var sut = CreateSut();
            IParser<IGeoCircle> geoCircleParser = new GeoCircleParser(0);

            var expectedResult = geoCircleParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<IGeoCircle>(stringValue);

            Assert.Equal(expectedResult.Latitude, result.Latitude);
            Assert.Equal(expectedResult.Longitude, result.Longitude);
            Assert.Equal(expectedResult.Radius, result.Radius);

            var fallback = default(IGeoCircle);
            result = sut.ParseOrFallback<IGeoCircle>("random");
            Assert.Equal(fallback, result);
        }
        
        
        [Fact]
        public void ParseOrFallback_OptionalDatabaseType()
        {
            const string stringValue = "SQLServer";
            var sut = CreateSut();
            var databaseTypeParser = new OptionalDatabaseTypeParser();

            var expectedResult = databaseTypeParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<DatabaseType?>(stringValue);

            Assert.Equal(expectedResult, result);

            var fallback = default(DatabaseType?);
            result = sut.ParseOrFallback<DatabaseType?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NotImplemented_throwException()
        {
            try
            {
                const string stringValue = "random";
                var sut = CreateSut();
                sut.ParseOrFallback<BigInteger>(stringValue);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }
        
        private static IAnyParser CreateSut()
        {
            return new AnySpecialParser();
        }
    }
}