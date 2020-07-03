using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.Converters.Basic
{
    public class DoubleConverter: IConverter<string, double>
    {
        public double Convert(string input)
        {
            return double.Parse(input, NumberStyles.Float, CultureInfo.InvariantCulture);
        }
    }
}
