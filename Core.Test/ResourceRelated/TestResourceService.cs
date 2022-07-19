using Core.Resources;
using Core.Test.Res;
using Xunit;

public class TestResourceService
{
    [Fact]
    public void BasicTest()
    {
        var mdFile = Resources.TestMarkdown();
        Assert.NotEmpty(mdFile);
    }

    [Fact]
    public void GetEmbeddedResourceNames()
    {
        var sut = ResourceService.FromType(typeof(TestResourceService));
        var allNames = sut.GetNames();
        Assert.Contains("Core.Test.Res.Samples.Test.md", allNames);
    }
}