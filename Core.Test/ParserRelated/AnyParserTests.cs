using System;
using System.Linq;
using System.Numerics;
using Core.Enums;
using Core.Mathematics;
using Core.Parser;
using Core.Parser.Basic;
using Core.Parser.Constants;
using Core.Parser.Special;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class AnyParserTests
    {
        [Fact]
        public void ParseOrFallback_Int()
        {
            const string stringValue = "1";
            var sut = CreateSut();
            var intParser = new IntegerParser();

            var expectedResult = intParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<int>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(int);
            result = sut.ParseOrFallback<int>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_Double()
        {
            const string stringValue = "1.56";
            var sut = CreateSut();
            var doubleParser = new DoubleParser();

            var expectedResult = doubleParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<double>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(double);
            result = sut.ParseOrFallback<double>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_DateTime()
        {
            const string stringValue = "2021-09-24T15:04:56.000Z";
            var sut = CreateSut();
            var dateTimeParser = new DateTimeParser();

            var expectedResult = dateTimeParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<DateTime>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(DateTime);
            result = sut.ParseOrFallback<DateTime>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_Bool()
        {
            const string stringValue = "yes";
            var sut = CreateSut();
            var boolParser = new BoolParser();

            var expectedResult = boolParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<bool>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(bool);
            result = sut.ParseOrFallback<bool>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableInt()
        {
            const string stringValue = "2";
            var sut = CreateSut();
            var optionalIntParser = new OptionalIntParser();

            var expectedResult =optionalIntParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<int?>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(int?);
            result = sut.ParseOrFallback<int?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableDouble()
        {
            const string stringValue = "1.56";
            var sut = CreateSut();
            var optionalDoubleParser = new OptionalDoubleParser();

            var expectedResult = optionalDoubleParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<double?>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(double?);
            result = sut.ParseOrFallback<double?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableDateTime()
        {
            const string stringValue = "2021-09-24T15:04:56.000Z";
            var sut = CreateSut();
            var optionalDateTimeParser = new OptionalDateTimeParser();

            var expectedResult = optionalDateTimeParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<DateTime?>(stringValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(DateTime?);
            result = sut.ParseOrFallback<DateTime?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableBool()
        {
            const string stringValue = "yes";
            var sut = CreateSut();
            var optionalBoolParser = new OptionalBoolParser();

            var expectedResult = optionalBoolParser.ParseOrFallback(stringValue);
            var result = sut.ParseOrFallback<bool?>(stringValue);

            Assert.Equal(expectedResult, result);

            var fallback = default(bool?);
            result = sut.ParseOrFallback<bool?>("random");
            Assert.Equal(fallback, result);
        }
        
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

            var expectedResult = databaseTypeParser.ParseOrFallback(stringValue);
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
   
        [Fact]
        public void ParseOrFallback_CanNotResolveParser_throwException()
        {
            try
            {
                const string stringValue = "1";
                var sut = new AnyParser(0, new AnyBasicParser(), new AnySpecialParser(), new[] {KnownDataTypes.Int},
                    new[] {KnownDataTypes.Int});
                sut.ParseOrFallback<int>(stringValue);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }
        private static IAnyParser CreateSut()
        {
            return new AnyParser();
        }
    }
}