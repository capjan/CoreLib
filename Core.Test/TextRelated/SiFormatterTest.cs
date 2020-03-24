using System.Globalization;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
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
    }
}