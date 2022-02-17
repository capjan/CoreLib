using Core.Extensions.TextRelated;
using Core.Text.Formatter.Mask;
using Core.Text.Formatter.MaskedText;
using Xunit;

namespace Core.Test.TextRelated;

public class MaskedTextFormatterTest
{
    [Theory]
    [InlineData('*', 5, "secret", "*****")]
    [InlineData('*', 5, "", "")]
    [InlineData('*', 5, null, "")]
    [InlineData('!', 3, "not-intend-for-public-099890", "!!!")]
    public void BasicMaskedCharsTest(char escapeChar, int maskLength, string input, string expected)
    {
        var replaceMask = new MaskedCharsMask(escapeChar, maskLength);
        var sut = new MaskedTextFormatter(replaceMask);

        Assert.Equal(expected, sut.WriteToString(input));
    }

    [Theory]
    [InlineData(default, "Secret", ReplacementTextMask.InitialDefaultReplacement)]
    [InlineData("53cR37", "Secret", "53cR37")]
    [InlineData("TEST", "Secret", "TEST")]
    [InlineData(default, "not-intend-for-public-099890", ReplacementTextMask.InitialDefaultReplacement)]
    [InlineData(default, "", "")]
    [InlineData(default, null, "")]
    public void BasicReplaceMaskTest(string replacement, string input, string expected)
    {
        var replaceMask = new ReplacementTextMask(replacement);
        var sut = new MaskedTextFormatter(replaceMask);

        Assert.Equal(expected, sut.WriteToString(input));
    }
}
