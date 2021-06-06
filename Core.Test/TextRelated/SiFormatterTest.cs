using System.Globalization;
using System.IO;
using System.Text;
using Core.Enums;
using Core.Extensions.TextRelated;
using Core.Text.Formatter;
using Xunit;

namespace Core.Test.TextRelated
{
    public class SiFormatterTest
    {
        [Fact]
        public void BasicTest()
        {
            var formatter = new SiFormatter {FormatProvider = CultureInfo.InvariantCulture};
            var std = formatter.WriteToString(144e4m);
            Assert.Equal("1.44 M", std);
        }

        [Fact]
        public void DoubleToStringTest()
        {
            var    formatter     = new SiFormatter
            {
                FormatProvider = CultureInfo.InvariantCulture, 
                SignificantDecimalPlaces = 1,
                ShortenStrategy = NumberShortenStrategy.Round
            };
            double doubleValue   = 123.4567;
            var    formattedText = formatter.WriteToString(doubleValue);
            Assert.Equal("123.5", formattedText);
        }

        [Fact]
        public void DoubleWriteTest()
        {
            var    formatter   = new SiFormatter
            {
                FormatProvider = CultureInfo.InvariantCulture,
                SignificantDecimalPlaces = 1,
                ShortenStrategy = NumberShortenStrategy.Round
            };
            double doubleValue = 123.4567;
            var    sb          = new StringBuilder();
            var    tw          = new StringWriter(sb);
            formatter.Write(doubleValue, tw);
            var formattedText = sb.ToString();
            Assert.Equal("123.5", formattedText);
        }
    }
}