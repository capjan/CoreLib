using System;
using Core.Mathematics;
using Xunit;

namespace Core.Test.MathematicsRelated;

public class DoubleDetailsTest
{
    [Fact]
    public void BasicTest()
    {
        var details = new DoubleDetails(123.456);
        Assert.Equal(123, details.IntegralPart);
        Assert.Equal(0.456, Math.Round(details.FractionPart, 3));
    }
}