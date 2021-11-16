using System;
using System.Numerics;
using Core.Parser;
using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class AnyBasicParserTests
    {
        [Fact]
        public void ParseOrFallback_Int()
        {
            const string stringValue = "1";
            var sut = CreateSut();
            var intParser = new IntegerParser();

            var expectedResult = intParser.ParseOrFallback(stringValue, 0);
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

            var expectedResult = doubleParser.ParseOrFallback(stringValue, default);
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

            var expectedResult = dateTimeParser.ParseOrFallback(stringValue, default);
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

            var expectedResult = boolParser.ParseOrFallback(stringValue, default);
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
            return new AnyBasicParser();
        }
    }
}