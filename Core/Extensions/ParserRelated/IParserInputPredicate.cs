using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Parser;

namespace Core.Extensions.ParserRelated;

internal static class InputPredicateUtil
{
    public static bool TryPeekOnMatch(IParserInput input, TextWriter writer, Func<char, bool> matchPredicate)
    {
        var isPeekSuccessful = input.TryPeekChar(out var peekedCharacter);
        if (!isPeekSuccessful) return false;
        var isMatchSuccessful = matchPredicate(peekedCharacter);
        if (!isMatchSuccessful)
        {
            input.LookaheadCount -= 1;
            return false;
        }
        writer.Write(peekedCharacter);
        return true;
    }
}

/// <summary>
/// Assertion Predicate that assert that the given predicate is a match WITHOUT writing it contents to the given writer.
/// </summary>
internal class AssertionPredicate : IParserInputPredicate
{
    private readonly IParserInputPredicate _predicate;
    private readonly TextWriter _nullWriter = new StreamWriter(Stream.Null);
    
    public AssertionPredicate(IParserInputPredicate predicate)
    {
        _predicate = predicate;
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        return _predicate.IsMatch(input, _nullWriter);
    }
}

internal class LogicalOrPredicate : IParserInputPredicate
{
    private readonly IParserInputPredicate[] _predicates;

    public LogicalOrPredicate(params IParserInputPredicate[] predicates)
    {
        _predicates = predicates;
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        foreach (var predicate in _predicates)
            if (predicate.IsMatch(input, writer))
                return true;

        return false;
    }
}

internal class IsMatchCharacterRange : IParserInputPredicate
{
    private readonly char _lowerCharacter;
    private readonly char _upperCharacter;

    public IsMatchCharacterRange(char lowerCharacter, char upperCharacter)
    {
        if (lowerCharacter > upperCharacter)
            throw new ArgumentException(
                message: $"{nameof(lowerCharacter)} must be lesser then or equal to {nameof(upperCharacter)}",
                paramName: nameof(lowerCharacter));

        _lowerCharacter = lowerCharacter;
        _upperCharacter = upperCharacter;
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        return InputPredicateUtil.TryPeekOnMatch(
            input, 
            writer, 
            matchPredicate: ch => _lowerCharacter <= ch && _upperCharacter >= ch);
    }
}

internal class IsMatchPredicate : IParserInputPredicate
{
    private readonly SortedSet<char> _matchSet;

    public IsMatchPredicate(params char[] chars)
    {
        _matchSet = new SortedSet<char>(chars);
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        return InputPredicateUtil.TryPeekOnMatch(
            input, 
            writer, 
            matchPredicate: ch => _matchSet.Contains(ch));
    }
}

internal class IsNoMatchPredicate : IParserInputPredicate
{
    private readonly SortedSet<char> _notMatchSet;
    
    public IsNoMatchPredicate(params char[] chars)
    {
        _notMatchSet = new SortedSet<char>(chars);
    }
    
    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        return InputPredicateUtil.TryPeekOnMatch(
            input, 
            writer, 
            matchPredicate: ch => !_notMatchSet.Contains(ch));
    }
}

internal class IsMatchingAnyStringPredicate : IParserInputPredicate
{
    private readonly IReadOnlyCollection<string> _values;
    private readonly IsMatchStringPredicate _matchStringPredicate;

    public IsMatchingAnyStringPredicate(IReadOnlyCollection<string> values, bool ignoreCase)
    {
        _values = values;
        _matchStringPredicate = new IsMatchStringPredicate(string.Empty, ignoreCase);
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        foreach (var value in _values)
        {
            _matchStringPredicate.Value = value;
            if (_matchStringPredicate.IsMatch(input, writer))
            {
                return true;
            }
        }

        return false;
    }
}

internal class IsMatchStringPredicate : IParserInputPredicate
{
    public string Value;
    private readonly bool _ignoreCase;

    public IsMatchStringPredicate(string value, bool ignoreCase)
    {
        Value = value;
        _ignoreCase = ignoreCase;
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        var lookaheadCount = input.LookaheadCount;
        var cachedStringBuilder = new StringBuilder();
        foreach (var ch in Value)
        {
            if (!input.TryPeekChar(out var peedChar))
            {
                input.LookaheadCount = lookaheadCount;
                return false;
            }

            if (_ignoreCase)
            {
                if (char.ToUpperInvariant(ch) != char.ToUpperInvariant(peedChar))
                {
                    input.LookaheadCount = lookaheadCount;
                    return false;
                }

                cachedStringBuilder.Append(peedChar);
                continue;
            }

            if (ch == peedChar)
            {
                cachedStringBuilder.Append(peedChar);
                continue;
            }
            input.LookaheadCount = lookaheadCount;
            return false;
        }

        writer.Write(cachedStringBuilder.ToString());
        return true;
    }
}

internal class RepeatPredicate : IParserInputPredicate
{
    private readonly IParserInputPredicate _predicateToRepeat;
    private readonly Repetition _repetition;

    public RepeatPredicate(IParserInputPredicate predicateToRepeat, Repetition repetition)
    {
        _predicateToRepeat = predicateToRepeat;
        _repetition = repetition;
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        if (_repetition.Minimum == 0 && (_repetition.Maximum > 0 || _repetition.Maximum < 0))
            return RepeatZeroOrMoreTimes(input, writer);
        if (_repetition.Minimum == 1 && (_repetition.Maximum > _repetition.Minimum || _repetition.Maximum < 0))
            return RepeatOneOrMoreTimes(input, writer);
        if (_repetition.Minimum > 1 && (_repetition.Maximum >= _repetition.Minimum || _repetition.Maximum < 0))
            return RepeatCustom(input, writer);
        if (_repetition.Minimum == 0 && _repetition.Maximum <= _repetition.Minimum)
            return true;
        return false;
    }

    private bool RepeatCustom(IParserInput input, TextWriter writer)
    {
        // repeat for an exact count or fail
        var successfulIterations = 0;
        var lookahead = input.LookaheadCount;
        var successWriter = new StringBuilder();
        var bufferedWriter = new StringWriter(successWriter);
        for (var i = 0; i < _repetition.Maximum; i++)
        {
            if (!_predicateToRepeat.IsMatch(input, bufferedWriter))
            {
                if (successfulIterations < _repetition.Minimum)
                {
                    input.LookaheadCount = lookahead;
                    return false;    
                }
                break;
            }

            successfulIterations++;
        }
        writer.Write(successWriter.ToString());
        return true;
    }

    private bool RepeatOneOrMoreTimes(IParserInput input, TextWriter writer)
    {
        if (!_predicateToRepeat.IsMatch(input, writer)) return false;
        while (_predicateToRepeat.IsMatch(input, writer)) { }
        return true;
    }

    private bool RepeatZeroOrMoreTimes(IParserInput input, TextWriter writer)
    {
        while (_predicateToRepeat.IsMatch(input, writer)) { }
        return true;
    }
}

internal class PredicateConcatenation : IParserInputPredicate
{
    private readonly List<IParserInputPredicate> _predicates = new();

    public PredicateConcatenation(params IParserInputPredicate[] predicates)
    {
        _predicates.AddRange(predicates);
    }

    public void Add(IParserInputPredicate predicate)
    {
        _predicates.Add(predicate);
    }

    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        if (_predicates.Count == 0) return true;
        var oldLookahead = input.LookaheadCount;
        var sb = new StringBuilder();
        using var sw = new StringWriter(sb);
        
        foreach (var predicate in _predicates)
        {
            if (predicate.IsMatch(input, sw)) continue;
            input.LookaheadCount = oldLookahead;
            return false;
        }
        
        writer.Write(sb.ToString());
        return true;
    }
}

internal class Predicate : IParserInputPredicate
{
    public static readonly IParserInputPredicate Empty = new Predicate(); 
    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        return true;
    }
    
    private Predicate() { }
}

public class InputPredicateBuilder : IPredicateBuilder
{
    public IParserInputPredicate Predicate { get; private set; } = Core.Extensions.ParserRelated.Predicate.Empty;

    public IPredicateBuilder Assert(Action<IPredicateBuilder> block)
    {
        var subBuilder = new InputPredicateBuilder();
        block(subBuilder);
        var blockPredicate = subBuilder.Predicate;
        var assertionPredicate = new AssertionPredicate(blockPredicate);
        AppendPredicate(assertionPredicate);
        return this;
    }
    
    public IPredicateBuilder Equals(char character)
    {
        var isMatchPredicate = new IsMatchPredicate(character);
        AppendPredicate(isMatchPredicate);
        return this;
    }

    public IPredicateBuilder Equals(string value)
    {
        var matchPredicate = new IsMatchStringPredicate(value, false);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public IPredicateBuilder Equals(string value, bool ignoreCase)
    {
        var matchPredicate = new IsMatchStringPredicate(value, ignoreCase);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public IPredicateBuilder Equals(string value, Repetition repetition)
    {
        var matchPredicate = new IsMatchStringPredicate(value, false);
        var repeatPredicate = new RepeatPredicate(matchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }
    
    public IPredicateBuilder Equals(string value, bool ignoreCase, Repetition repetition)
    {
        var matchPredicate = new IsMatchStringPredicate(value, ignoreCase);
        var repeatPredicate = new RepeatPredicate(matchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }
    
    public IPredicateBuilder Equals(char character, Repetition repetition)
    {
        var isMatchPredicate = new IsMatchPredicate(character);
        var repeatPredicate = new RepeatPredicate(isMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public IPredicateBuilder Equals(char[] characterSet)
    {
        var isMatchPredicate = new IsMatchPredicate(characterSet);
        AppendPredicate(isMatchPredicate);
        return this;
    }

    public IPredicateBuilder Equals(char[] characterSet, Repetition repetition)
    {
        var isMatchPredicate = new IsMatchPredicate(characterSet);
        var repeatPredicate = new RepeatPredicate(isMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public IPredicateBuilder EqualsNot(char character)
    {
        var noMatchPredicate = new IsNoMatchPredicate(character);
        AppendPredicate(noMatchPredicate);
        return this;
    }

    public IPredicateBuilder EqualsNot(char character, Repetition repetition)
    {
        var noMatchPredicate = new IsNoMatchPredicate(character);
        var repeatPredicate = new RepeatPredicate(noMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public IPredicateBuilder EqualsNot(char[] characterSet)
    {
        var noMatchPredicate = new IsNoMatchPredicate(characterSet);
        AppendPredicate(noMatchPredicate);
        return this;
    }

    public IPredicateBuilder EqualsNot(char[] characterSet, Repetition repetition)
    {
        var noMatchPredicate = new IsNoMatchPredicate(characterSet);
        var repeatPredicate = new RepeatPredicate(noMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public IPredicateBuilder EqualsCharacterRange(char lowerBound, char upperBound)
    {
        var isMatchPredicate = new IsMatchCharacterRange(lowerBound, upperBound);
        AppendPredicate(isMatchPredicate);
        return this;
    }
    
    public IPredicateBuilder EqualsCharacterRange(char lowerBound, char upperBound, Repetition repetition)
    {
        var isMatchPredicate = new IsMatchCharacterRange(lowerBound, upperBound);
        var repeatPredicate = new RepeatPredicate(isMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public IPredicateBuilder EqualsAny(params string[] values)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, false);
        AppendPredicate(matchPredicate);
        return this;
    }
    public IPredicateBuilder EqualsAny(IReadOnlyCollection<string> values)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, false);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public IPredicateBuilder EqualsAny(IReadOnlyCollection<string> values, bool ignoreCase)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, ignoreCase);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public IPredicateBuilder EqualsAny(IReadOnlyCollection<string> values, bool ignoreCase, Repetition repetition)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, ignoreCase);
        var repetitionPredicate = new RepeatPredicate(matchPredicate, repetition);
        AppendPredicate(repetitionPredicate);
        return this;
    }

    public IPredicateBuilder EqualsWordBoundary()
    {
        var matchPredicate = new IsMatchingWordBoundaryPredicate();
        AppendPredicate(matchPredicate);
        return this;
    }

    public IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder> logicalOrBuilder)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2);
        AppendPredicate(logicalOrPredicate);
        return this;
    }
    
    public IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        var builder3 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2, builder3);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var predicate3 = builder3.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2, predicate3);
        AppendPredicate(logicalOrPredicate);
        return this;
    }
    
    public IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        var builder3 = new InputPredicateBuilder();
        var builder4 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2, builder3, builder4);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var predicate3 = builder3.Predicate;
        var predicate4 = builder4.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2, predicate3, predicate4);
        AppendPredicate(logicalOrPredicate);
        return this;
    }
    
    public IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        var builder3 = new InputPredicateBuilder();
        var builder4 = new InputPredicateBuilder();
        var builder5 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2, builder3, builder4, builder5);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var predicate3 = builder3.Predicate;
        var predicate4 = builder4.Predicate;
        var predicate5 = builder4.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2, predicate3, predicate4, predicate5);
        AppendPredicate(logicalOrPredicate);
        return this;
    }

    public IPredicateBuilder EqualsAny(Action<IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2);
        var repetitionPredicate = new RepeatPredicate(logicalOrPredicate, repetition);
        AppendPredicate(repetitionPredicate);
        return this;
    }

    public IPredicateBuilder EqualsAny(Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        var builder3 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2, builder3);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var predicate3 = builder3.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2, predicate3);
        var repetitionPredicate = new RepeatPredicate(logicalOrPredicate, repetition);
        AppendPredicate(repetitionPredicate);
        return this;
    }

    public IPredicateBuilder EqualsAny(Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        var builder3 = new InputPredicateBuilder();
        var builder4 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2, builder3, builder4);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var predicate3 = builder3.Predicate;
        var predicate4 = builder4.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2, predicate3, predicate4);
        var repetitionPredicate = new RepeatPredicate(logicalOrPredicate, repetition);
        AppendPredicate(repetitionPredicate);
        return this;
    }

    public IPredicateBuilder EqualsAny(Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition)
    {
        var builder1 = new InputPredicateBuilder();
        var builder2 = new InputPredicateBuilder();
        var builder3 = new InputPredicateBuilder();
        var builder4 = new InputPredicateBuilder();
        var builder5 = new InputPredicateBuilder();
        logicalOrBuilder(builder1, builder2, builder3, builder4, builder5);
        var predicate1 = builder1.Predicate;
        var predicate2 = builder2.Predicate;
        var predicate3 = builder3.Predicate;
        var predicate4 = builder4.Predicate;
        var predicate5 = builder4.Predicate;
        var logicalOrPredicate = new LogicalOrPredicate(predicate1, predicate2, predicate3, predicate4, predicate5);
        var repetitionPredicate = new RepeatPredicate(logicalOrPredicate, repetition);
        AppendPredicate(repetitionPredicate);
        return this;
    }

    private void AppendPredicate(IParserInputPredicate predicate)
    {
        if (Predicate is PredicateConcatenation list)
        {
            list.Add(predicate);
        }
        else if (Predicate == Core.Extensions.ParserRelated.Predicate.Empty)
        {
            Predicate = predicate;
        }
        else
        {
            var temp = new PredicateConcatenation(Predicate, predicate);
            Predicate = temp;
        }
    }
}

public class IsMatchingWordBoundaryPredicate : IParserInputPredicate
{
    public bool IsMatch(IParserInput input, TextWriter writer)
    {
        if (input.TryPeekChar(out var nextChar))
            input.LookaheadCount--; // remove the lookahead count, because this match is of zero length
        else
            nextChar = default;
        
        var isLastCharAlphanumeric = char.IsLetterOrDigit(input.LastCharacter);
        var isNextCharAlphanumeric = char.IsLetterOrDigit(nextChar);

        var differentCase1 = isLastCharAlphanumeric && !isNextCharAlphanumeric;
        var differentCase2 = !isLastCharAlphanumeric && isNextCharAlphanumeric; 
        return differentCase1 || differentCase2;
    }
}
