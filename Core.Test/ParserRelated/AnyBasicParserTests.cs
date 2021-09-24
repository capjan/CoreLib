using System;
using System.Numerics;
using Core.Parser.Basic;
using Core.Parser.Basic.Interfaces;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class AnyBasicParserTests
    {
        [Fact]
        public void ParseOrFallback_Int()
        {
            var stringValue = "1";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<int>(stringValue);

            Assert.Equal(1, result);

            var fallback = default(int);
            result = sut.ParseOrFallBack<int>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_Double()
        {
            var stringValue = "1.56";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<double>(stringValue);

            Assert.Equal(1.56, result);

            var fallback = default(double);
            result = sut.ParseOrFallBack<double>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_DateTime()
        {
            var checkTime = new DateTime(2021, 09, 24, 15, 04, 56);
            var stringValue = "2021-09-24T15:04:56.000Z";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<DateTime>(stringValue);

            Assert.Equal(checkTime, result);

            var fallback = default(DateTime);
            result = sut.ParseOrFallBack<DateTime>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_Bool()
        {
            var stringValue = "yes";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<bool>(stringValue);

            Assert.True(result);

            var fallback = default(bool);
            result = sut.ParseOrFallBack<bool>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableInt()
        {
            var stringValue = "2";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<int?>(stringValue);

            Assert.Equal(2, result);
            
            var fallback = default(int?);
            result = sut.ParseOrFallBack<int?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableDouble()
        {
            var stringValue = "1.56";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<double?>(stringValue);

            Assert.Equal(1.56, result);

            var fallback = default(double?);
            result = sut.ParseOrFallBack<double?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableDateTime()
        {
            var checkTime = new DateTime(2021, 09, 24, 15, 04, 56);
            var stringValue = "2021-09-24T15:04:56.000Z";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<DateTime?>(stringValue);

            Assert.Equal(checkTime, result);

            var fallback = default(DateTime?);
            result = sut.ParseOrFallBack<DateTime?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NullableBool()
        {
            var stringValue = "yes";
            var sut = CreateSut();
            var result = sut.ParseOrFallBack<bool?>(stringValue);

            Assert.True(result);

            var fallback = default(bool?);
            result = sut.ParseOrFallBack<bool?>("random");
            Assert.Equal(fallback, result);
        }
        
        [Fact]
        public void ParseOrFallback_NotImplemented_throwException()
        {
            try
            {
                var stringValue = "random";
                var sut = CreateSut();
                sut.ParseOrFallBack<BigInteger>(stringValue);
            }
            catch (Exception e)
            {
                Assert.NotNull(e);
            }
        }

        private IAnyParser CreateSut()
        {
            return new AnyBasicParser();
        }
    }
}