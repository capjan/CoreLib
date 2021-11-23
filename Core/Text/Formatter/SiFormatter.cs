using System;
using System.IO;
using Core.Enums;
using Core.Extensions.MathematicsRelated;

namespace Core.Text.Formatter
{
    public class SiFormatter : ISiFormatter
    {
        private readonly char[] _incPrefixes = { 'k', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y' };
        private readonly char[] _decPrefixes = { 'm', '\u03bc', 'n', 'p', 'f', 'a', 'z', 'y' };

        public void Write(decimal value, TextWriter writer)
        {
            // handle special case 0
            if (value == 0)
            {
                WriteScaledValue(writer,"0", null);
                return;
            }

            var degree = ForcedDegree ?? CalculateDegree(value);

            if (degree < -_decPrefixes.Length)
            {
                // result falls below min
                WriteScaledValue(writer, "< 1",'y');
                return;
            }

            if (degree > _incPrefixes.Length)
            {
                // result exceeds max limit
                var valueAsString = value > 0m ? "> 999" : "< -999";
                WriteScaledValue(writer, valueAsString,'Y');
                return;
            }

            var scaled  = value * Convert.ToDecimal(Math.Pow(1000, -degree));

            if (SignificantDecimalPlaces.HasValue)
            {
                switch (ShortenStrategy)
                {
                    case NumberShortenStrategy.Round:
                        scaled = Math.Round(scaled, SignificantDecimalPlaces.Value, MidpointRounding.AwayFromZero);
                        break;
                    case NumberShortenStrategy.Truncate:
                    case NumberShortenStrategy.Default:
                        scaled = scaled.TruncateAfterDecimalPlace(SignificantDecimalPlaces.Value);
                        break;
                }
            }


            char? prefix = null;
            switch (Math.Sign(degree))
            {
                case 1:  prefix = _incPrefixes[degree - 1]; break;
                case -1: prefix = _decPrefixes[-degree - 1]; break;
            }

            var valueStr = scaled.ToString(Format, FormatProvider);
            WriteScaledValue(writer, valueStr, prefix);
        }

        private void WriteScaledValue(TextWriter writer, string valueStr, char? prefix)
        {
            writer.Write(valueStr);
            if (!string.IsNullOrEmpty(Delimiter) && (prefix != null || !string.IsNullOrWhiteSpace(Unit)))
                writer.Write(Delimiter);
            writer.Write(prefix);
            writer.Write(Unit);
        }

        private static int CalculateDegree(decimal value)
        {
            var log10 = Math.Log10(Convert.ToDouble(Math.Abs(value)));
            var result = (int) Math.Floor(log10 / 3);

            return result;
        }

        public string                Format                   { get; set; } = "0.###";
        public IFormatProvider?       FormatProvider           { get; set; }
        public string                Delimiter                { get; set; } = " ";
        public int?                  ForcedDegree             { get; set; }
        public string Unit { get; set; } = "";
        public int?                  SignificantDecimalPlaces { get; set; }
        public NumberShortenStrategy ShortenStrategy          { get; set; }
    }
}
