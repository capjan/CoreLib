using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Converters.Basic
{
    public class DoubleConverter: IConverter<string, double>
    {
        private const string GermanToInvariantNumberPattern = @"^(?<signs>\s*-\s*)*(?<number>\d+)(?<grouped>(\.\d{3})*)(?<fraction>,\d+)\s*$";
        public double Convert(string input)
        {
            var usedInput = input;
            var m = Regex.Match(input, GermanToInvariantNumberPattern);
            if (m.Success)
            {
                var sb = new StringBuilder();
                if (m.Groups["signs"].Success)
                    sb.Append(m.Groups["signs"].Value);
                sb.Append(m.Groups["number"].Value);
                if (m.Groups["grouped"].Success)
                    sb.Append(m.Groups["grouped"].Value.Replace(".", ""));
                sb.Append(m.Groups["fraction"].Value.Replace(",", "."));
                usedInput = sb.ToString();
            }

            usedInput = usedInput.Replace(",", "");
            return double.Parse(usedInput, NumberStyles.Float, CultureInfo.InvariantCulture);
        }
    }
}
