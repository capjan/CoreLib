using System;
using System.Collections.Generic;
using System.Text;
using Core.Extensions.CollectionRelated;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
using Xunit;

namespace Core.Test.TextRelated
{
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

            var formatter = new DefaultSeparatorFormatter<int>(
                separator: ", ",
                groupLength: 1,
                toStringFunc: v => v.ToString(),
                nullPlaceholder: "");

            formatter.Write(intArray, Console.Out);
            var formatted = formatter.WriteToString(intArray);

            // or use the extension method on any IEnumerable<T>
            var formattedString = formatter.WriteToString(intArray);

            var formattedViaExtension = intArray.ToSeparatedString();

            Assert.Equal("one, two, three", new []{"one", "two", "three"}.ToSeparatedString());
        }
    }
}
