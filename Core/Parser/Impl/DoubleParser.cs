using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Parser.Impl
{
    public class OptionalDoubleParser: IParser<double?>
    {
        public double? ParseOrFallback(string input, double? fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            input = input.Trim();
            return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedDouble) ? parsedDouble : fallback;
        }
    }

    public class DoubleParser : IParser<double>
    {
        public double ParseOrFallback(string input, double fallback = default)
        {
            if (string.IsNullOrWhiteSpace(input)) return fallback;
            input = input.Trim();
            return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedDouble) ? parsedDouble : fallback;
        }
    }
}
