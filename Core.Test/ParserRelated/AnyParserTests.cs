using System;
using System.Globalization;
using Core.Parser.AnyParser;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class AnyParserTests
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("123", 123)]
        [InlineData("739", 739)]
        [InlineData("852 ", 852)]
        [InlineData(" 12 ", 12)]
        public void Parse_Int(string input, int expected)
        {
            var sut = CreateSut();

            var actual = sut.Parse<int>(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1, 2)]
        [InlineData("Nonsense", 123, 123)]
        [InlineData("", 85, 85)]
        [InlineData("741", 741, 963)]
        [InlineData("000", 0, 963)]
        [InlineData("0001", 1, 963)]
        [InlineData("Hallo0001", 963, 963)]
        [InlineData("123 456", 123456, 741)] // We allow spaces as separators in numbers
        public void ParseOrFallback_Int(string input, int expected, int fallback)
        {
            var sut = CreateSut();

            var actual = sut.ParseOrFallback<int>(input, fallback);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1.0, 2.0)]
        [InlineData("Nonsense", 123.0, 123.0)]
        [InlineData("", 8.5, 8.5)]
        [InlineData("74.1", 74.1, 96.3)]
        [InlineData("74,1", 74.1, 96.3)]
        [InlineData("7 4,1", 74.1, 96.3)]
        [InlineData("000", 0.0, 9.63)]
        [InlineData("0001", 1.0, 9.63)]
        [InlineData("Hallo0001", 96.3, 96.3)]
        [InlineData("123 456", 123456.0, 7.41)] // We allow spaces as separators in numbers
        public void ParseOrFallback_Double(string input, double expected, double fallback)
        {
            var sut = CreateSut();

            var actual = sut.ParseOrFallback(input, fallback);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1", 1.0)]
        [InlineData("8.5  ", 8.5)]
        [InlineData("74.1 ", 74.1)]
        [InlineData("74,1", 74.1)]
        [InlineData("7 4,1", 74.1)]
        [InlineData("000", 0.0)]
        [InlineData(" 0001", 1.0)]
        [InlineData("123 456", 123456.0)] // We allow spaces as separators in numbers
        public void Parse_Double(string input, double expected)
        {
            var sut = CreateSut();

            var actual = sut.Parse<double>(input);
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("2021-09-24T15:04:56.000Z", "2021-09-24T15:04:56.000Z", "1986-04-24T15:16:56.421Z")]
        [InlineData("Nonsense", "2021-09-24T15:04:56.000Z", "2021-09-24T15:04:56.000Z")]
        public void ParseOrFallback_DateTime(string input, string expectedString, string fallbackString)
        {
            var sut = CreateSut();

            var expected = DateTime.Parse(expectedString, null, DateTimeStyles.AssumeUniversal).ToUniversalTime();
            var fallback = DateTime.Parse(fallbackString, null, DateTimeStyles.AssumeUniversal).ToUniversalTime();

            var actual = sut.ParseOrFallback(input, fallback);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("true", true, false)]
        [InlineData("TRUE", true, false)]
        [InlineData("True", true, false)]
        [InlineData("TRUe", true, false)]
        [InlineData("yes", true, false)]
        [InlineData("1", true, false)]
        [InlineData(" TRUe ", true, false)]
        [InlineData("false", false, true)]
        [InlineData("False", false, true)]
        [InlineData("NO", false, true)]
        [InlineData("0", false, true)]
        public void ParseOrFallback_Bool(string input, bool expected, bool fallback)
        {
            var sut = CreateSut();
            var actual = sut.ParseOrFallback(input, fallback);
            Assert.Equal(expected, actual);
        }

        private static IAnyParser CreateSut()
        {
            return new CoreAnyParser();
        }
    }
}
