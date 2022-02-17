using System.Collections.ObjectModel;
using Core.Collections;
using Core.Extensions.CollectionRelated;
using Xunit;

namespace Core.Test.CollectionsRelated;

public class CompositeReadonlyListTest
{
    [Fact]
    public void BasicTest()
    {
        var first  = new[] {1, 2, 3};
        var second = new[] {4, 5, 6};

        var readonly1 = new ReadOnlyCollection<int>(first);
        var readonly2 = new ReadOnlyCollection<int>(second);

        var compositeList = new CompositeReadOnlyList<int>(readonly1, readonly2);
        Assert.Equal(6, compositeList.Count);
        for (var i = 0; i < compositeList.Count; i++)
            Assert.Equal(i + 1, compositeList[i]);
        Assert.Equal("1, 2, 3, 4, 5, 6", compositeList.ToSeparatedString());
    }
}
