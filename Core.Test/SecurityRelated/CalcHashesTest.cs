using Core.Extensions.SecurityRelated;
using Xunit;

namespace Core.Test.SecurityRelated;

public class CalcHashesTest
{
    [Fact]
    public void CalcMD5Test()
    {
        const string email = "john.doe@domain.com";
        var hashedEmail = email.CalcMD5();
        Assert.Equal("5B9E16F1A64D33E2F7ABF2FCE24E9B87", hashedEmail);
    }
}