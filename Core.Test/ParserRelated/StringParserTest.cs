using Core.Parser.Special;
using Core.Parser.TokenParser;
using Xunit;

namespace Core.Test.ParserRelated;

public class StringParserTest
{
    [Fact]
    public void BasicTest()
    {
        var parser = new StringTokenParser();

        using (var input = ParserInput.CreateFromString("\"Hello World!\""))
            Assert.Equal("Hello World!", parser.Parse(input));

        using (var input = ParserInput.CreateFromString("\"\\\\\""))
            Assert.Equal("\\", parser.Parse(input));

        parser.EscapedCharLookup.Add('t', '\t');

        using (var input = ParserInput.CreateFromString("\"\\tHello\""))
            Assert.Equal("\tHello", parser.Parse(input));

    }

    [Fact]
    public void DoubleQuotationTest()
    {
        var doubleQuoteStringParser = new StringTokenParser( true);
        using (var input = ParserInput.CreateFromString("\"abc\"\"123\""))
            Assert.Equal("abc\"123", doubleQuoteStringParser.Parse(input));

        var singleQuoteParser = new StringTokenParser(true, '\'');
        using (var input = ParserInput.CreateFromString("'abc''123'"))
            Assert.Equal("abc'123", singleQuoteParser.Parse(input));
    }

}