using System;
using System.IO;
using Core.Enums;

namespace Core.Text.Formatter.Impl
{
    /// <summary>
    /// Implements a File Size Formatter
    /// </summary>
    public class FileSizeFormatter : IFileSizeFormatter
    {
        private IFormattableTextFormatter<double> _numberFormatter = new DefaultNumberFormatter<double>();

        private const long _kib = 1024L;
        private const long _mib = 1024 * _kib;
        private const long _gib = 1024 * _mib;
        private const long _tib = 1024 * _gib;
        private const long _pib = 1024 * _tib;      

        public FileSizeFormatter()
        {
        }

        public string Format {
            get => _numberFormatter.Format;
            set => _numberFormatter.Format = value;
        }

        public IFormatProvider FormatProvider {
            get => _numberFormatter.FormatProvider;
            set => _numberFormatter.FormatProvider = value;
        }

        public BinaryUnitPrefix? ForcedUnit { get; set; } = null;
        public string Delimiter { get; set; } = " ";

        public void Write(long value, TextWriter writer)
        {
            var dividerInfo = GetDeviderInfo(value);
            var result = value / dividerInfo.Denominator;            
            var delimiter = Delimiter ?? "";

            _numberFormatter.Write(result, writer);            
            writer.Write($"{delimiter}{dividerInfo.Prefix}B");
        }

        private (string Prefix, double Denominator) GetDeviderInfo(long value)
        {
            if (ForcedUnit.HasValue)
            {
                switch (ForcedUnit.Value)
                {
                    case BinaryUnitPrefix.Kibi: return ("Ki", _kib);
                    case BinaryUnitPrefix.Mebi: return ("Mi", _mib);
                    case BinaryUnitPrefix.Gibi: return ("Gi", _gib);
                    case BinaryUnitPrefix.Tebi: return ("Ti", _tib);
                    case BinaryUnitPrefix.Pebi: return ("Pi", _pib);
                    default: throw new InvalidOperationException($"The given value '{Enum.GetName(typeof(BinaryUnitPrefix), ForcedUnit)}' for the property '{nameof(ForcedUnit)}' is unexpected/not implemented.");
                }
            }
            if (value >= _pib) return ("Pi", _pib);
            if (value >= _tib) return ("Ti", _tib);
            if (value >= _gib) return ("Gi", _gib);
            if (value >= _mib) return ("Mi", _mib);
            if (value >= _kib) return ("Ki", _kib);
            return ("", 1);
        }
    }
}
