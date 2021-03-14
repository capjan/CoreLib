using Core.Extensions.TextRelated;
using Core.Text.Formatter;
using Xunit;

namespace Core.Test.TextRelated
{
    public class NumberFormatterTest
    {
        private const char Micro = '\u03bc';

        [Fact]
        public void TestSiFormatter()
        {
            var formatter = new SiFormatter();
            formatter.MakeCultureInvariant();

            Assert.Equal("1 k", formatter.WriteToString(1000));
            Assert.Equal("10", formatter.WriteToString(10));
            Assert.Equal("1 M", formatter.WriteToString(1000000));
            formatter.Format = "n2";
            Assert.Equal("1.44 M", formatter.WriteToString(1440000));
            formatter.Delimiter = "\t";
            Assert.Equal("1.44\tM", formatter.WriteToString(1440000));
            formatter.Delimiter = null;
            Assert.Equal("1.44M", formatter.WriteToString(1440000));
            formatter.Format = "0.##";
            Assert.Equal($"150{Micro}", formatter.WriteToString(1.5e-4m));
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
            formatter.Format = "0.######";
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
            formatter.Format = "0.######";
            Assert.Equal("0.000123 MB", formatter.WriteToString(123));
            formatter.AutoScale(); // auto degree
            Assert.Equal("123 B", formatter.WriteToString(123));
        }

        [Fact]
        public void TestFormattingRoundingGlitch()
        {
            var formatter = new SiFormatter {Format = "0.##"};
            formatter.MakeCultureInvariant();

            // because the formatting limits the decimal places to 2 decimal places the formatting rounds up to 1000
            Assert.Equal("1000 k", formatter.WriteToString(999999));

            // To prevent automatic rounding via formatting you can limit the significant decimal places to the
            // given number of digits.
            formatter.SignificantDecimalPlaces = 2;
            Assert.Equal("999.99 k", formatter.WriteToString(999999));
        }

        [Fact]
        public void TestSiFormatterEdges()
        {
            var formatter = new SiFormatter {Format = "0.##"};
            formatter.MakeCultureInvariant();

            Assert.Equal("0", formatter.WriteToString(0));

            // int min/max
            Assert.Equal("2.15 G", formatter.WriteToString(int.MaxValue));
            Assert.Equal("-2.15 G", formatter.WriteToString(int.MinValue));

            // double min/max
            Assert.Equal("> 999 Y", formatter.WriteToString(decimal.MaxValue));
            Assert.Equal("< -999 Y", formatter.WriteToString(decimal.MinValue));
        }

        [Fact]
        public void TestSiFractions()
        {
            var formatter = new SiFormatter {Format = "0.##"};
            formatter.MakeCultureInvariant();

            Assert.Equal("1", formatter.WriteToString(1));
            Assert.Equal("1 m", formatter.WriteToString(1e-3m));
            Assert.Equal("999 m", formatter.WriteToString(999e-3m));
            Assert.Equal($"1 {Micro}", formatter.WriteToString(1e-6m));
            Assert.Equal("1 n", formatter.WriteToString(1e-9m));
            Assert.Equal("1 p", formatter.WriteToString(1e-12m));
            Assert.Equal("1 f", formatter.WriteToString(1e-15m));
            Assert.Equal("1 a", formatter.WriteToString(1e-18m));
            Assert.Equal("1 z", formatter.WriteToString(1e-21m));
            Assert.Equal("1 y", formatter.WriteToString(  1e-24m));
            Assert.Equal("999 y", formatter.WriteToString(999e-24m));
            Assert.Equal("< 1 y", formatter.WriteToString(1e-27M));
        }

        [Fact]
        public void TestSiMultiples()
        {
            var formatter = new SiFormatter {Format = "0.##"};
            formatter.MakeCultureInvariant();

            Assert.Equal("1", formatter.WriteToString(1));
            Assert.Equal("999", formatter.WriteToString(999));
            Assert.Equal("1 k", formatter.WriteToString(1e3m));
            Assert.Equal("1 M", formatter.WriteToString(1e6m));
            Assert.Equal("1 G", formatter.WriteToString(1e9m));
            Assert.Equal("1 T", formatter.WriteToString(1e12m));
            Assert.Equal("1 P", formatter.WriteToString(1e15m));
            Assert.Equal("1 E", formatter.WriteToString(1e18m));
            Assert.Equal("1 Z", formatter.WriteToString(1e21m));
            Assert.Equal("1 Y", formatter.WriteToString(1e24m));
            Assert.Equal("999 Y", formatter.WriteToString(999e24m));
            Assert.Equal("> 999 Y", formatter.WriteToString(1e27m));
        }

        [Fact]
        public void TestForceExtensions()
        {
            var formatter = new SiFormatter();
            formatter.MakeCultureInvariant(); // extension to make the formatter culture invariant
            formatter.ForceMega();

            var result = formatter.WriteToString(144e4m);
            // result contains 1 k
            Assert.Equal("1.44 M", result);
        }

    }
}
