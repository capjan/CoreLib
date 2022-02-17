using System;
using Core.Extensions.CollectionRelated;
using Core.Extensions.TextRelated;
using Core.Text.Formatter;
using Xunit;

namespace Core.Test.TextRelated;

public class SeparatorFormatterTest
{
    [Fact]
    public void BasicTest()
    {
        Assert.Equal("one, two, three", new []{"one", "two", "three"}.ToSeparatedString());
    }

    [Fact]
    public void ExampleTest()
    {
        var intArray = new[] {1, 2, 3, 4, 5};

        var formatter = new SeparatorFormatter<int>();

        formatter.Write(intArray, Console.Out);
        var formatted = formatter.WriteToString(intArray);
        Assert.Equal("1, 2, 3, 4, 5", formatted);

        // or use the extension method on any IEnumerable<T>
        var formattedViaExtension = intArray.ToSeparatedString();
        Assert.Equal(formatted, formattedViaExtension);

        Assert.Equal("one, two, three", new []{"one", "two", "three"}.ToSeparatedString());
    }

    [Fact]
    public void TestCollectionSeparator()
    {
        Assert.Equal("T, e, s, t", 
            "Test".ToCharArray()
                .ToSeparatedString());
    }
}