using Core.Mathematics.Impl;
using Xunit;

namespace Core.Test.MathematicsRelated;

public class TestRandom
{
    [Fact]
    public void BasicRandomTest()
    {
        var random = new DefaultRandom(12345);
        var next = random.Next();
        Assert.Equal(143337951, next);
        next = random.Next();
        Assert.Equal(150666398, next);
        next = random.Next();
        Assert.Equal(1663795458, next);
    }

    [Fact]
    public void RandomMaxValueTest()
    {
        var random = new DefaultRandom();
        for (var i = 0; i < 1000; i++)
        {
            var next = random.Next(10);
            Assert.InRange(next, 0,10);
        }            
    }

    [Fact]
    public void RandomMinMaxValueTest()
    {
        var random = new DefaultRandom();
        for (var i = 0; i < 1000; i++)
        {
            var next = random.Next(10, 20);
            Assert.InRange(next, 10,20);
        }            
    }

    [Fact]
    public void RandomMinMaxValueTest2()
    {
        var random = new DefaultRandom();
        var hitCount = new int[4];
        for (var i = 0; i < 1000; i++)
        {
            var next = random.Next(0, 3);
            hitCount[next]++;
        }
        Assert.True(hitCount[0] > 0);
        Assert.True(hitCount[3] > 0);
    }
}