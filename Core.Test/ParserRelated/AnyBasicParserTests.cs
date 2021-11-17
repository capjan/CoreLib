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
            int result = sut.Parse(stringValue, 0);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(int);
            result = sut.Parse("random", 0);
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_Double()
        {
            const string stringValue = "1.56";
            var sut = CreateSut();
            var doubleParser = new DoubleParser();

            var expectedResult = doubleParser.ParseOrFallback(stringValue, default);
            double result = sut.Parse(stringValue, 0.0);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(double);
            result = sut.Parse("random", 0.0);
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_DateTime()
        {
            const string stringValue = "2021-09-24T15:04:56.000Z";
            var sut = CreateSut();
            var dateTimeParser = new DateTimeParser();

            var expectedResult = dateTimeParser.ParseOrFallback(stringValue, default);
            var result = sut.Parse(stringValue, DateTime.MinValue);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(DateTime);
            result = sut.Parse("random", DateTime.MinValue);
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_Bool()
        {
            const string stringValue = "yes";
            var sut = CreateSut();
            var boolParser = new BoolParser();

            var expectedResult = boolParser.ParseOrFallback(stringValue, default);
            var result = sut.Parse(stringValue, false);

            Assert.Equal(expectedResult, result);
            
            var fallback = default(bool);
            result = sut.Parse("random", false);
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableInt()
        {
            const string stringValue = "2";
            var sut = CreateSut();
            var optionalIntParser = new OptionalIntParser();

            var expectedResult =optionalIntParser.ParseOrFallback(stringValue);
            var result = sut.Parse(stringValue, default(int?));

            Assert.Equal(expectedResult, result);
            
            var fallback = default(int?);
            result = sut.Parse("random", default(int));
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableDouble()
        {
            const string stringValue = "1.56";
            var sut = CreateSut();
            var optionalDoubleParser = new OptionalDoubleParser();

            var expectedResult = optionalDoubleParser.ParseOrFallback(stringValue);
            var result = sut.Parse(stringValue, default(double?));

            Assert.Equal(expectedResult, result);
            
            var fallback = default(double?);
            result = sut.Parse("random", default(double?));
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableDateTime()
        {
            const string stringValue = "2021-09-24T15:04:56.000Z";
            var sut = CreateSut();
            var optionalDateTimeParser = new OptionalDateTimeParser();

            var expectedResult = optionalDateTimeParser.ParseOrFallback(stringValue);
            var result = sut.Parse(stringValue, default(DateTime?));

            Assert.Equal(expectedResult, result);
            
            var fallback = default(DateTime?);
            result = sut.Parse("random", default(DateTime?));
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableBool()
        {
            const string stringValue = "yes";
            var sut = CreateSut();
            var optionalBoolParser = new OptionalBoolParser();

            var expectedResult = optionalBoolParser.ParseOrFallback(stringValue);
            var result = sut.Parse(stringValue, default(bool?));

            Assert.Equal(expectedResult, result);

            var fallback = default(bool?);
            result = sut.Parse("random", default(bool?));
            Assert.Equal(fallback, result);
        }
        //
        // [Fact]
        // public void ParseOrFallback_NotImplemented_throwException()
        // {
        //     try
        //     {
        //         const string stringValue = "random";
        //         var sut = CreateSut();
        //         sut.Parse(stringValue, new BigInteger(123));
        //     }
        //     catch (Exception e)
        //     {
        //         Assert.NotNull(e);
        //     }
        // }

        private static IAnyParser CreateSut()
        {
            return new AnyParser();
        }
    }
}