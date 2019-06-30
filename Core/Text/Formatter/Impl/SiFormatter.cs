using System;
using System.IO;

namespace Core.Text.Formatter.Impl
{
    public class SiFormatter : ISiFormatter
    {
        private readonly char[] _incPrefixes = { 'k', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y' };
        private readonly char[] _decPrefixes = { 'm', '\u03bc', 'n', 'p', 'f', 'a', 'z', 'y' };
        
        public void Write(double value, TextWriter writer)
        {
            var degree = ForcedDegree ?? (int) Math.Floor(Math.Log10(Math.Abs(value)) / 3);
            var scaled = value * Math.Pow(1000, -degree);

            char? prefix = null;
            switch (Math.Sign(degree))
            {
                case 1:  prefix = _incPrefixes[degree - 1]; break;
                case -1: prefix = _decPrefixes[-degree - 1]; break;
            }

            writer.Write(scaled.ToString(Format, FormatProvider));
            if (!string.IsNullOrEmpty(Delimiter) && (prefix != null || Unit != null))
                writer.Write(Delimiter);
            writer.Write(prefix);
            writer.Write(Unit);
        }
        
        public string Format { get; set; }
        public IFormatProvider FormatProvider { get; set; }
        public string Delimiter { get; set; } = " ";
        public int? ForcedDegree { get; set; }
        public string Unit { get; set; }
    }
}