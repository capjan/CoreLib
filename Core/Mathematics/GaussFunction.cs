using System;

namespace Core.Mathematics;

/// <summary>
/// Well known Gauss Function from Carl Friedrich Gauss. See: https://en.wikipedia.org/wiki/Gaussian_function
/// </summary>
public class GaussFunction
{
    private readonly double _a;
    private readonly double _b;
    private readonly double _c;

    /// <summary>
    /// Calculates the Y-Position for the given x coordinate
    /// </summary>
    /// <param name="x">x-position</param>
    /// <returns></returns>
    public double CalculateY(double x)
    {
        var v1 = (x - _b) / (2d * _c * _c);
        var v2 = -v1 * v1 / 2d;
        var v3 = _a * Math.Exp(v2);

        return v3;
    }

    /// <summary>
    /// Creates a new gauss function with the given characteristics.
    /// </summary>
    /// <param name="a">height of the bell curves peak</param>
    /// <param name="b">x position of the center of the peak</param>
    /// <param name="c">standard deviation. the width of the curve. must be non zero</param>
    public GaussFunction(double a, double b, double c)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (c == 0) throw new ArgumentException("with of the bell curve must not be zero.", nameof(c));
        _a = a;
        _b = b;
        _c = c;
    }
}