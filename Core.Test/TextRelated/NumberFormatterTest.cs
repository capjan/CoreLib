using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
    public class NumberFormatterTest
    {
        [Fact]
        public void TestSiFormatter()
        {
            var formatter = new SiFormatter();
            formatter.MakeCultureInvariant();
            var mu = '\u03bc'; // unicode: GREEK SMALL LETTER MU
            
            Assert.Equal("1 k", formatter.WriteToString(1000));
            Assert.Equal("10", formatter.WriteToString(10));
            Assert.Equal("1 M", formatter.WriteToString(1000000));
            formatter.Format = "n2";
            Assert.Equal("1.44 M", formatter.WriteToString(1440000));
            formatter.Delimiter = "\t";
            Assert.Equal("1.44\tM", formatter.WriteToString(1440000));
            formatter.Delimiter = null;
            Assert.Equal("1.44M", formatter.WriteToString(1440000));
            formatter.Format = null;
            Assert.Equal($"150{mu}", formatter.WriteToString(1.5e-4));
        }
        
        [Fact]
        public void TestSiFormatterWithForcedDegree()
        {
            var formatter = new SiFormatter();
            formatter.MakeCultureInvariant();
            formatter.ForcedDegree = 1; // force Kilo
            formatter.Delimiter = null; // remove delimiter between value and unit
            
            Assert.Equal("0.123k", formatter.WriteToString(123));
            formatter.ForcedDegree = 2; // force mega
            Assert.Equal("0.000123M", formatter.WriteToString(123));
            formatter.ForcedDegree = null; // auto degree
            Assert.Equal("123", formatter.WriteToString(123));
        }
        
        [Fact]
        public void TestSiFormatterWithUnit()
        {
            var formatter = new SiFormatter();
            formatter.MakeCultureInvariant();
            formatter.ForceKilo();
            formatter.Unit = "B"; // Byte
            
            Assert.Equal("0.123 kB", formatter.WriteToString(123));
            formatter.ForceMega();
            Assert.Equal("0.000123 MB", formatter.WriteToString(123));
            formatter.AutoScale(); // auto degree
            Assert.Equal("123 B", formatter.WriteToString(123));
        }

        [Fact]
        public void TestForceExtensions()
        {
            var formatter = new SiFormatter();
            formatter.MakeCultureInvariant(); // extension to make the formatter culture invariant
            formatter.ForceMega();

            var result = formatter.WriteToString(144e4);
            // result contains 1 k
            Assert.Equal("1.44 M", result);
        }
        
    }
}