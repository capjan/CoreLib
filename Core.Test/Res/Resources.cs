using Core.Parser;
using Core.Resources;

namespace Core.Test.Res;

public class Resources
{
    private static ResourceService R = ResourceService.FromType(typeof(Resources));

    public static string TestMarkdown() => R.GetStringByName("Core.Test.Res.Samples.Test.md");
}