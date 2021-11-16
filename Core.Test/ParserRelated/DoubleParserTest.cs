﻿using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated
{
    public class DoubleParserTest
    {
        [Fact]
        public void BasicDoubleParserTest()       
        {
            var parser = new DoubleParser();
            Assert.Equal(1.234, parser.ParseOrFallback("1.234", default));
            Assert.Equal(1.234, parser.ParseOrFallback("1,234", default));
            Assert.Equal(1234.0, parser.ParseOrFallback("1234", default));
            Assert.Equal(123.45, parser.ParseOrFallback("123,45", default));
            Assert.Equal(1234.56, parser.ParseOrFallback("1.234,56", default));
            Assert.Equal(1234.56, parser.ParseOrFallback("1,234.56", default));
            Assert.Equal(0.0, parser.ParseOrFallback("0", default));
            Assert.Equal(-123.45, parser.ParseOrFallback("-123.45", default));
            Assert.Equal(-123.45, parser.ParseOrFallback("-123,45", default));
            Assert.Equal(-1234.5, parser.ParseOrFallback("-1,234.5", default));
            Assert.Equal(-1234.5, parser.ParseOrFallback("-1.234,5", default));
        }
    }
}

