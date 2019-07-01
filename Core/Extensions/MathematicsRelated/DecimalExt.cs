using System;

namespace Core.Extensions.MathematicsRelated
{
    public static class DecimalExt
    {
        public static decimal TruncateAfterDecimalPlace(this decimal value, int decimalPlace)
        {
            var step = (decimal)Math.Pow(10, decimalPlace);
            var tmp  = Math.Truncate(step * value);
            return tmp / step;
        }
    }
}