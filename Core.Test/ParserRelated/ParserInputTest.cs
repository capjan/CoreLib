using System;
using System.IO;
using Core.Extensions.ParserRelated;
using Core.Parser;
using Core.Parser.Special;
using Core.Text.Impl;
using Xunit;
// ReSharper disable StringLiteralTypo

namespace Core.Test.ParserRelated;

public class ParserInputTest
{
    [Fact]
    public void BasicStringInputTest()
    {
        const string source = "abcdef 12345";
        using var input = ParserInput.CreateFromString(source);
        Assert.Equal(0, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        var pos1 = TextPosition.Start;

        // lookahead 1
        Assert.Equal(pos1, input.TextPosition);
        Assert.True(input.TryPeekChar(out var ch));
        Assert.Equal('a', ch);
        Assert.Equal(pos1, input.TextPosition);
        Assert.Equal(1, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        // lookahead 2
        Assert.True(input.TryPeekChar(out ch));
        Assert.Equal('b', ch);
        Assert.Equal(pos1, input.TextPosition);
        Assert.Equal(2, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        // read the first char. This should reset the lookup
        Assert.True(input.TryReadChar(out ch));
        Assert.Equal('a', ch);
        Assert.Equal(1, input.TextPosition.LineNumber);
        Assert.Equal(2, input.TextPosition.ColumnNumber);
        Assert.Equal(0, input.LookaheadCount);
        Assert.Equal(1, input.Offset);

        // lookahead 1
        Assert.True(input.TryPeekChar(out ch));
        Assert.Equal('b', ch);
        Assert.Equal(1, input.TextPosition.LineNumber);
        Assert.Equal(2, input.TextPosition.ColumnNumber);
        Assert.Equal(1, input.LookaheadCount);
        Assert.Equal(1, input.Offset);

        // read next char
        Assert.True(input.TryReadChar(out ch));
        Assert.Equal('b', ch);
        Assert.Equal(1, input.TextPosition.LineNumber);
        Assert.Equal(3, input.TextPosition.ColumnNumber);
        Assert.Equal(0, input.LookaheadCount);
        Assert.Equal(2, input.Offset);
    }

    [Fact]
    public void LastPeekedCharacterTest()
    {
        var input = ParserInput.CreateFromString("ABCDEF");
        Assert.Equal( default, input.LastCharacter);
        Assert.Equal(0, input.Offset);
        
        Assert.True(input.TryPeekChar(out var chA));
        Assert.Equal('A', chA);
        Assert.Equal('A', input.LastCharacter);
        Assert.Equal(0, input.Offset);
        
        Assert.True(input.TryPeekChar(out var chB));
        Assert.Equal('B', chB);
        Assert.Equal('B', input.LastCharacter);
        Assert.Equal(0, input.Offset);

        input.LookaheadCount = 0;
        Assert.Equal(default, input.LastCharacter);
        Assert.Equal(0, input.Offset);

        input.LookaheadCount = 1;
        Assert.Equal('A', input.LastCharacter);
        Assert.Equal(0, input.Offset);
        
        input.LookaheadCount = 2;
        Assert.Equal('B', input.LastCharacter);
        Assert.Equal(0, input.Offset);
        
        Assert.True(input.TryReadChar(out var readChA));
        Assert.Equal('A', readChA);
        Assert.Equal('A', input.LastCharacter);
        Assert.Equal(1, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        Assert.True(input.TryReadChar(out var readChB));
        Assert.Equal('B', readChB);
        Assert.Equal('B', input.LastCharacter);
        Assert.Equal(2, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
    }

    [Fact]
    public void BasicLookupTest()
    {
        const string source = "abcdef 12345";
        using var input = ParserInput.CreateFromString(source);
        Assert.Equal(0, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        var pos1 = TextPosition.Start;

        // lookahead 1
        Assert.Equal(pos1, input.TextPosition);
        Assert.True(input.TryPeekChar(out var ch));
        Assert.Equal('a', ch);
        Assert.Equal(pos1, input.TextPosition);
        Assert.Equal(1, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        // lookahead 2
        Assert.True(input.TryPeekChar(out ch));
        Assert.Equal('b', ch);
        Assert.Equal(pos1, input.TextPosition);
        Assert.Equal(2, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        input.LookaheadCount = 0; // clears the current lookahead
        Assert.Equal(0, input.LookaheadCount);
        Assert.Equal(pos1, input.TextPosition);

        // lookahead 1
        Assert.Equal(pos1, input.TextPosition);
        Assert.True(input.TryPeekChar(out ch));
        Assert.Equal('a', ch);
        Assert.Equal(pos1, input.TextPosition);
        Assert.Equal(1, input.LookaheadCount);
        Assert.Equal(0, input.Offset);

        // lookahead 2
        Assert.True(input.TryPeekChar(out ch));
        Assert.Equal('b', ch);
        Assert.Equal(pos1, input.TextPosition);
        Assert.Equal(2, input.LookaheadCount);
        Assert.Equal(0, input.Offset);
    }

    [Fact]
    public void PeekReadLookaheadTest()
    {
        var input = ParserInput.CreateFromString("01234");

        Assert.True(input.TryPeekChar(out var ch0) && ch0 == '0');
        Assert.Equal(0, input.Offset);
        Assert.Equal(1, input.LookaheadCount);
        
        Assert.True(input.TryPeekChar(out var ch1) && ch1 == '1');
        Assert.Equal(0, input.Offset);
        Assert.Equal(2, input.LookaheadCount);
        
        Assert.True(input.TryPeekChar(out var ch2) && ch2 == '2');
        Assert.Equal(0, input.Offset);
        Assert.Equal(3, input.LookaheadCount);
        
        Assert.True(input.TryPeekChar(out var ch3) && ch3 == '3');
        Assert.Equal(0, input.Offset);
        Assert.Equal(4, input.LookaheadCount);
        
        Assert.True(input.TryPeekChar(out var ch4) && ch4 == '4');
        Assert.Equal(0, input.Offset);
        Assert.Equal(5, input.LookaheadCount);

        input.LookaheadCount = 3;
        Assert.True(input.TryPeekChar(out var ch5) && ch5 == '3');
        Assert.Equal(0, input.Offset);
        Assert.Equal(4, input.LookaheadCount);
        
        input.ReadLookahead(); // Read "0123"
        Assert.Equal(4, input.Offset);
        Assert.Equal(5, input.TextPosition.ColumnNumber);
        
        Assert.True(input.TryPeekChar(out var ch6) && ch6 == '4');
        Assert.Equal(4, input.Offset);
        Assert.Equal(5, input.TextPosition.ColumnNumber);
        
        Assert.True(input.TryReadChar(out var ch7) && ch7 == ch6);
        Assert.Equal(5, input.Offset);
        Assert.Equal(6, input.TextPosition.ColumnNumber);
    }

    [Fact]
    public void BasicTestTryPeekMatch()
    {
        var input = ParserInput.CreateFromString("01234");
        
        Assert.True(input.TryPeekMatch('0'));
        Assert.True(input.TryPeekMatch('1'));
        Assert.True(input.TryPeekMatch('2'));
        Assert.True(input.TryPeekMatch('3'));
        Assert.False(input.TryPeekMatch('3'));
        Assert.False(input.TryPeekMatch('5'));
        Assert.True(input.TryPeekMatch('4'));
        Assert.False(input.TryPeekMatch('5'));
    }
    
    [Fact]
    public void AdvancedTestTryPeekMatch()
    {
        var input = ParserInput.CreateFromString("01234");
        
        Assert.True(input.TryPeekMatch('0'));
        Assert.True(input.TryPeekMatch('1'));
        Assert.True(input.TryPeekMatch('2'));
        Assert.True(input.TryPeekMatch('3'));
        Assert.False(input.TryPeekMatch('3'));
        Assert.False(input.TryPeekMatch('5'));
        Assert.True(input.TryPeekMatch('4'));
        Assert.False(input.TryPeekMatch('5'));

        input.LookaheadCount = 2; // peeked "01"
        Assert.True(input.TryPeekMatch('2'));
        Assert.Equal(0, input.Offset);
        Assert.True(input.TryReadChar(out var readCh) && readCh == '0');
        Assert.Equal(1, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
    }
    
    [Fact]
    public void TestTryReadMatch()
    {
        var input = ParserInput.CreateFromString("01234");
        
        Assert.True(input.TryPeekMatch('0'));
        Assert.True(input.TryPeekMatch('1'));
        Assert.True(input.TryPeekMatch('2'));
        Assert.True(input.TryPeekMatch('3'));
        Assert.False(input.TryPeekMatch('3'));
        Assert.False(input.TryPeekMatch('5'));
        Assert.True(input.TryPeekMatch('4'));
        Assert.False(input.TryPeekMatch('5'));

        input.LookaheadCount = 2; // peeked "01"
        Assert.True(input.TryPeekMatch('2'));
        Assert.Equal(0, input.Offset);
        Assert.Equal(3, input.LookaheadCount); // Lookahead must count 3 characters
        Assert.False(input.TryReadMatch('1')); // expected fail
        Assert.Equal(3, input.LookaheadCount); // Lookahead must not change due to a failed TryReadMatch
        Assert.True(input.TryReadMatch('0')); // expected success
        Assert.Equal(1, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
    }
    
    [Fact]
    public void TestTryReadMatchCharacterRange()
    {
        var predicate = ParserInput.Predicate()
            .Equals('<')
            .EqualsCharacterRange('a', 'z')
            .EqualsCharacterRange('0', '9')
            .Equals('>')
            .Predicate;
        

        var input = ParserInput.CreateFromString("<h1>");
        
        Assert.True(input.TryPeekMatch(predicate, out var readString));
        Assert.Equal("<h1>", readString);
        Assert.Equal(0, input.Offset);
        Assert.Equal(4, input.LookaheadCount);
    }
    
    [Fact]
    public void TestRepetition()
    {
        var input = ParserInput.CreateFromString("AAA_BBB");
        
        // Predicate that matches: A+
        var predicate = ParserInput.Predicate()
            .Equals('A', Repetition.OneOrMore)
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate, out var peeked));
        Assert.Equal("AAA", peeked);
        Assert.Equal(0, input.Offset);
        Assert.Equal(3, input.LookaheadCount);
        
        // Read the Lookahead
        input.ReadLookahead();
        Assert.Equal(3, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        // Check the next Character
        Assert.True(input.TryReadChar(out var chUnderline));
        Assert.Equal('_', chUnderline);
        Assert.Equal(4, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        Assert.True(input.TryPeekMatch('B'));
    }
    
    [Fact]
    public void TestPredicate1()
    {
        var input = ParserInput.CreateFromString("AAA_BBB");
        
        // Predicate that matches the RegEx: A+_B{3}
        var predicate = ParserInput.Predicate()
            .Equals('A', Repetition.OneOrMore)
            .Equals('_')
            .Equals('B', Repetition.Exact(3))
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate, out var peeked));
        Assert.Equal("AAA_BBB", peeked);
        Assert.Equal(0, input.Offset);
        Assert.Equal(7, input.LookaheadCount);
    }
    
    [Fact]
    public void TestPredicate2()
    {
        var input = ParserInput.CreateFromString("123_AAA_BBB");

        var skipPredicate = ParserInput.Predicate()
            .Equals(new[] {'1', '2', '3', '_'}, Repetition.OneOrMore)
            .Predicate;
            
        Assert.True(input.TryPeekMatch(skipPredicate, out var prefix));
        Assert.Equal("123_", prefix);
        Assert.Equal(0, input.Offset);
        Assert.Equal(4, input.LookaheadCount);
        
        input.ReadLookahead();
        Assert.Equal(4, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        // Predicate that matches the RegEx: A+_B{3}
        var predicate = ParserInput.Predicate()
            .Equals('A', Repetition.OneOrMore)
            .Equals('_')
            .Equals('B', Repetition.Exact(3))
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate, out var peeked));
        Assert.Equal("AAA_BBB", peeked);
        Assert.Equal(4, input.Offset);
        Assert.Equal(7, input.LookaheadCount);
    }

    [Fact]
    public void TestAssertion()
    {
        var input = ParserInput.CreateFromString("@<h1>Headline");

        var predicate = ParserInput.Predicate()
            .Assert(block => block.Equals('<'))
            .EqualsCharacterRange('a', 'z')
            .EqualsCharacterRange('0', '9')
            .Assert(block => block.Equals('>'))
            .Predicate;
        
        Assert.True(input.TryReadChar(out var chAt) && chAt == '@');
        Assert.Equal(1, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        Assert.True(input.TryReadMatch(predicate, out var matchingString));
        Assert.Equal("h1", matchingString);
        Assert.Equal(5, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        Assert.True(input.TryReadChar(out var chH) && chH == 'H');
    }
    
    [Fact]
    public void TestFailedAssertion()
    {
        var input = ParserInput.CreateFromString("@<H1>Headline");

        var predicate = ParserInput.Predicate()
            .Assert(block => block.Equals('<'))
            .EqualsCharacterRange('a', 'z')
            .EqualsCharacterRange('0', '9')
            .Assert(block => block.Equals('>'))
            .Predicate;
        
        Assert.True(input.TryReadChar(out var chAt) && chAt == '@');
        Assert.Equal(1, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        // This Try Read must fail, because the H1 tag does not match because it is uppercase
        Assert.False(input.TryReadMatch(predicate, out _));
        Assert.Equal(1, input.Offset);
        Assert.Equal(0, input.LookaheadCount);
        
        Assert.True(input.TryReadChar(out var chLt) && chLt == '<');
    }

    [Fact]
    public void TestEqualsString()
    {
        var input = ParserInput.CreateFromString("abc123");

        var predicate = ParserInput.Predicate()
            .Equals("abc", false)
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate, out var peekedString));
        Assert.Equal("abc", peekedString);
        Assert.Equal(0, input.Offset);
        Assert.Equal(3, input.LookaheadCount);

        var predicate2 = ParserInput.Predicate()
            .Equals("123")
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate2, out var peekedString2));
        Assert.Equal("123", peekedString2);
        Assert.Equal(0, input.Offset);
        Assert.Equal(6, input.LookaheadCount);
    }

    [Fact]
    public void TestEqualString2()
    {
        var input = ParserInput.CreateFromString("aBc123");

        var predicate = ParserInput.Predicate()
            .Equals("abc", true)
            .Equals("123")
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate, out var peekedString));
        Assert.Equal("aBc123", peekedString);
        Assert.Equal(0, input.Offset);
        Assert.Equal(6, input.LookaheadCount);
    }
    
    [Fact]
    public void TestEqualAnyString()
    {
        var input = ParserInput.CreateFromString("if (a == b)");

        var keywords = new [] {"new", "or", "if"};
        var predicate = ParserInput.Predicate()
            .EqualsAny(keywords)
            .Predicate;
        
        Assert.True(input.TryPeekMatch(predicate, out var peekedString));
        Assert.Equal("if", peekedString);
        Assert.Equal(0, input.Offset);
        Assert.Equal(2, input.LookaheadCount);
    }

    [Fact]
    public void TestLogicalOr()
    {
        // Regex: (John|Peter)
        var predicate = ParserInput.Predicate()
            .EqualsAny((option1, option2) =>
            {
                option1.Equals("John");
                option2
                    .Equals('P')
                    .Equals('e')
                    .Equals('t')
                    .Equals('e')
                    .Equals('r');
            })
            .Predicate;

        var input1 = ParserInput.CreateFromString("John");
        var input2 = ParserInput.CreateFromString("Peter");
        var input3 = ParserInput.CreateFromString("Thomas");
        
        Assert.True(input1.TryReadMatch(predicate, out var john));
        Assert.Equal("John", john);
        
        Assert.True(input2.TryPeekMatch(predicate, out var peter));
        Assert.Equal("Peter", peter);
        
        Assert.False(input3.TryPeekMatch(predicate, out _));
    }

    [Fact]
    public void EscapedStringTest()
    {
        // predicate for a string with possibility to escape " with \" to prevent closing the string
        var predicate = ParserInput.Predicate()
            .Equals('"')
            .EqualsAny((option1, option2) =>
            {
                option1.Equals("\\\"");
                option2.EqualsNot('"');
            }, Repetition.ZeroOrMore)
            .Equals('"')
            .Predicate;

        var input1 = ParserInput.CreateFromString("\"Hello\"");
        Assert.True(input1.TryReadMatch(predicate, out var normalString));
        Assert.Equal("\"Hello\"", normalString);
        
        var input2 = ParserInput.CreateFromString("\"Hello \\\"World\\\"\"");
        Assert.True(input2.TryReadMatch(predicate, out var escapedString));
        Assert.Equal("\"Hello \\\"World\\\"\"", escapedString);
    }

    [Fact]
    public void TestWordBoundaryPredicate1()
    {
        var predicate = ParserInput.Predicate()
            .EqualsWordBoundary()
            .EqualsAny("var", "do", "for")
            .EqualsWordBoundary()
            .Predicate;

        var input = ParserInput.CreateFromString("vardofor var do for");
        Assert.False(input.TryPeekMatch(predicate, out _));

        // skip 9 characters
        Assert.Equal(9,input.ReadString(9, out _));
        
        // match var
        Assert.True(input.TryReadMatch(predicate, out var varString));
        Assert.Equal("var", varString);
        Assert.True(input.TryReadChar(out _)); // skip whitespace
        
        // match do
        Assert.True(input.TryPeekMatch(predicate, out var doString));
        Assert.Equal("do", doString);
        input.ReadLookahead();
        
        Assert.True(input.TryReadChar(out _)); // skip whitespace
        
        // Match ending for
        Assert.True(input.TryReadMatch(predicate, out var forString));
        Assert.Equal("for", forString);
    }
    
    [Theory]
    [InlineData("", false)]
    [InlineData("(", false)]
    [InlineData("A", true)]
    [InlineData("1", true)]
    [InlineData("ä", true)]
    [InlineData("z", true)]
    public void TestWordBoundaryPredicate2(string inputString, bool expectedMatch)
    {
        var predicate = ParserInput.Predicate()
            .EqualsWordBoundary()
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        Assert.Equal(expectedMatch, input.TryPeekMatch(predicate, out _));
        Assert.Equal(0, input.Offset);
        // lookahead must be still zero, because the word boundary predicate is of zero length
        Assert.Equal(0, input.LookaheadCount);
    }

    [Fact]
    public void CtorTest()
    {
        var str = "Hello World";
        var stringReader = new StringReader(str);
        var input = new ParserInput(stringReader);
        Assert.NotNull(input);
    }

    [Theory]
    [InlineData("1", true)]
    [InlineData("-1", true)]
    [InlineData("--1", false)]
    [InlineData("-", false)]
    public void TestZeroOrOneQuantifier(string inputString, bool expectedIsMatch)
    {
        var zeroOrOne = ParserInput.Predicate()
            .Equals('-', Repetition.ZeroOrOne)
            .Equals('1')
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        Assert.Equal(expectedIsMatch, input.TryPeekMatch(zeroOrOne, out _));
    }
    
    [Theory]
    [InlineData("1", true)]
    [InlineData("-1", true)]
    [InlineData("--1", true)]
    [InlineData("---1", true)]
    [InlineData("-----------1", true)]
    [InlineData("-", false)]
    public void TestZeroOrMoreQuantifier(string inputString, bool expectedIsMatch)
    {
        var zeroOrOne = ParserInput.Predicate()
            .Equals('-', Repetition.ZeroOrMore)
            .Equals('1')
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        Assert.Equal(expectedIsMatch, input.TryPeekMatch(zeroOrOne, out _));
    }
    
    [Theory]
    [InlineData("1", false)]
    [InlineData("-1", true)]
    [InlineData("--1", true)]
    [InlineData("---1", true)]
    [InlineData("-----------1", true)]
    [InlineData("-", false)]
    public void TestOneOrMoreQuantifier(string inputString, bool expectedIsMatch)
    {
        var zeroOrOne = ParserInput.Predicate()
            .Equals('-', Repetition.OneOrMore)
            .Equals('1')
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        Assert.Equal(expectedIsMatch, input.TryPeekMatch(zeroOrOne, out _));
    }
    
    [Theory]
    [InlineData("-1", 1, true)]
    [InlineData("--1", 1, false)]
    [InlineData("1", 1, false)]
    [InlineData("--1", 2, true)]
    [InlineData("---1", 2, false)]
    [InlineData("-1", 2, false)]
    [InlineData("---1", 3, true)]
    [InlineData("--1", 3, false)]
    [InlineData("-1", 3, false)]
    [InlineData("-----------1", 11, true)]
    [InlineData("-", 1, false)]
    public void TestExactQuantifier(string inputString, int quantity, bool expectedIsMatch)
    {
        var zeroOrOne = ParserInput.Predicate()
            .Equals('-', Repetition.Exact(quantity))
            .Equals('1')
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        Assert.Equal(expectedIsMatch, input.TryPeekMatch(zeroOrOne, out _));
    }

    [Fact]
    public void TestExceptionOnExact0Repetition()
    {
        // Exact 0 makes no sense
        Assert.Throws<ArgumentException>(() =>
        {
            Repetition.Exact(count: 0);
        });
    }
    
    [Fact]
    public void TestExceptionOnNegativeExactRepetition()
    {
        // Exact with negative numbers must fail 
        Assert.Throws<ArgumentException>(() =>
        {
            Repetition.Exact(count: -1);
        });
    }
    
    [Fact]
    public void TestExceptionOnNegativeMinimumRangeRepetition()
    {
        // Range with negative minimum must fail 
        Assert.Throws<ArgumentException>(() =>
        {
            Repetition.Range(minimum: -1, maximum: 5);
        });
    }
    
    [Fact]
    public void TestExceptionOnMaximumLesserThanMinimumRangeRepetition()
    {
        // Range with maximum greater than minimum must fail 
        Assert.Throws<ArgumentException>(() =>
        {
            Repetition.Range(minimum: 4, maximum: 3);
        });
    }
    
    [Theory]
    [InlineData("1", 0, 1, true)]
    [InlineData("-1", 0, 1, true)]
    [InlineData("--1", 0, 1, false)]
    [InlineData("-1", 1, 2, true)]
    [InlineData("--1", 1, 2, true)]
    [InlineData("1", 1, 2, false)]
    [InlineData("---1", 1, 2, false)]
    public void TestRangeQuantifier(string inputString, int minimumQuantity, int maximumQuantity, bool expectedIsMatch)
    {
        var zeroOrOne = ParserInput.Predicate()
            .Equals('-', Repetition.Range(minimumQuantity, maximumQuantity))
            .Equals('1')
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        Assert.Equal(expectedIsMatch, input.TryPeekMatch(zeroOrOne, out _));
    }
    
    [Theory]
    [InlineData("123", true, "123")]
    [InlineData("+123", true, "+123")]
    [InlineData("-123", true, "-123")]
    [InlineData("123.567", true, "123.567")]
    [InlineData("+123.567", true, "+123.567")]
    [InlineData("-123.567", true, "-123.567")]
    [InlineData("-123..567", true, "-123")]
    [InlineData("+-123..567", false, "")]
    public void OptionalSubPredicateTest(string inputString, bool expectedIsMatch, string expectedRead)
    {
        // matches: -1, -1,123, 
        var floatPredicate = ParserInput.Predicate()
            .Equals(new [] {'-', '+'}, Repetition.ZeroOrOne)
            .EqualsWordBoundary()
            .EqualsCharacterRange('0', '9', Repetition.OneOrMore)
            .Equals(block => block
                    .Equals('.')
                    .EqualsCharacterRange('0', '9', Repetition.OneOrMore)
                , Repetition.ZeroOrOne)
            .EqualsWordBoundary()
            .Predicate;

        var input = ParserInput.CreateFromString(inputString);
        
        Assert.Equal(expectedIsMatch, input.TryReadMatch(floatPredicate, out var readMatch));
        Assert.Equal(expectedRead, readMatch);
    }
}