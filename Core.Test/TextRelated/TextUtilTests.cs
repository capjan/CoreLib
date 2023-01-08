using Core.Text;
using Xunit;

namespace Core.Test.TextRelated;

public class TextUtilTests
{
    [Fact]
    public void GenerateAlphanumericString()
    {
        var randomText = TextUtilities.CreateAlphanumericString(10);
        Assert.NotEmpty(randomText);
        Assert.Equal(10, randomText.Length);
    }
    
    [Fact]
    public void GenerateLoremIpsumText()
    {
        var loremIpsumText = TextUtilities.CreateLoremIpsumText(10);
        Assert.NotEmpty(loremIpsumText);
    }
}