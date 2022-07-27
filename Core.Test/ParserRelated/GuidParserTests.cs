using System;
using Core.Extensions.ParserRelated;
using Core.Parser.Basic;
using Xunit;

namespace Core.Test.ParserRelated;

public class GuidParserTests
{
    [Theory]
    [InlineData("4E77C966-4611-4148-B6FC-BA7C9DFDC4BD", true)]
    [InlineData("{44B0FBE8-5E9B-4819-8609-875A08AA46D5}", true)]
    [InlineData("A42FC31C669343208264900D2E4D0505", true)]
    [InlineData("WTF This is not a valid GUID", false)]
    public void BasicTest(string input, bool expectedSuccess)
    {
        var sut = new GuidParser();
        var parsedGuid = sut.ParseOrNull(input);
        if (expectedSuccess)
        {
            Assert.True(parsedGuid.HasValue);
            var expectedGuid = Guid.Parse(input);
            Assert.Equal(expectedGuid, parsedGuid);
        }
        else
        {
            Assert.False(parsedGuid.HasValue);
        }

    }
}