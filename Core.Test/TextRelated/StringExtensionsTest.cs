using Core.Extensions.TextRelated;
using Xunit;

namespace Core.Test.TextRelated;

public class StringExtensionsTest
{
    [Fact]
    public void RightTest()
    {
        Assert.Equal("World", "Hello World".Right(5));
        Assert.Equal("Hello World", "Hello World".Right(100));
    }

    [Fact]
    public void LeftTest()
    {
        Assert.Equal("Hell", "Hello World".Left(4));
        Assert.Equal("Hello World", "Hello World".Left(100));
    }
}