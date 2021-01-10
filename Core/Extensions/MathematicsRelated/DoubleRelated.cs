using Core.Mathematics;

namespace Core.Extensions.MathematicsRelated
{
    public static class DoubleRelated
    {
        /// <summary>
        /// Returns the Details of the Number like Integral and Fraction part.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DoubleDetails Details(this double value)
        {
            return new DoubleDetails(value);
        }
    }
}
