using System.Reflection;
using Core.Reflection;
using Core.Text;
using Xunit;

namespace Core.Test.ReflectionRelated;

public class AssemblyInfoTest
{
    [Fact]
    public void BasicTest()
    {
        var info = AssemblyInfo.FromType(typeof(TextUtilities));
        Assert.Contains("Ruhlaender", info.Company);
        Assert.Equal("CoreLib", info.Title);
        Assert.NotNull(info.Copyright);
        Assert.Equal("CoreLib", info.Product);
    }
}