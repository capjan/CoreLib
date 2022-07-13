using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Parser;

namespace Core.Extensions.ParserRelated;

public interface IParserInputPredicate
{
    /// <summary>
    /// Executes the given predicate and returns true on success otherwise nothing happens.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="writer"></param>
    /// <returns></returns>
    bool IsMatch(IParserInput input, TextWriter writer);
}

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

public class InputParserPredicateBuilder
{
    private IParserInputPredicate _predicate = Predicate.Empty;

    public InputParserPredicateBuilder Assert(Action<InputParserPredicateBuilder> block)
    {
        var subBuilder = new InputParserPredicateBuilder();
        block(subBuilder);
        var blockPredicate = subBuilder._predicate;
        var assertionPredicate = new AssertionPredicate(blockPredicate);
        AppendPredicate(assertionPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder Equals(char character)
    {
        var isMatchPredicate = new IsMatchPredicate(character);
        AppendPredicate(isMatchPredicate);
        return this;
    }

    public InputParserPredicateBuilder Equals(string value)
    {
        var matchPredicate = new IsMatchStringPredicate(value, false);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder Equals(string value, bool ignoreCase)
    {
        var matchPredicate = new IsMatchStringPredicate(value, ignoreCase);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder Equals(string value, Repetition repetition)
    {
        var matchPredicate = new IsMatchStringPredicate(value, false);
        var repeatPredicate = new RepeatPredicate(matchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder Equals(string value, bool ignoreCase, Repetition repetition)
    {
        var matchPredicate = new IsMatchStringPredicate(value, ignoreCase);
        var repeatPredicate = new RepeatPredicate(matchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder Equals(char character, Repetition repetition)
    {
        var isMatchPredicate = new IsMatchPredicate(character);
        var repeatPredicate = new RepeatPredicate(isMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public InputParserPredicateBuilder Equals(char[] characterSet, Repetition repetition)
    {
        var isMatchPredicate = new IsMatchPredicate(characterSet);
        var repeatPredicate = new RepeatPredicate(isMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder EqualsCharacterRange(char lowerBound, char upperBound)
    {
        var isMatchPredicate = new IsMatchCharacterRange(lowerBound, upperBound);
        AppendPredicate(isMatchPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder EqualsCharacterRange(char lowerBound, char upperBound, Repetition repetition)
    {
        var isMatchPredicate = new IsMatchCharacterRange(lowerBound, upperBound);
        var repeatPredicate = new RepeatPredicate(isMatchPredicate, repetition);
        AppendPredicate(repeatPredicate);
        return this;
    }

    public InputParserPredicateBuilder EqualsAny(params string[] values)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, false);
        AppendPredicate(matchPredicate);
        return this;
    }
    public InputParserPredicateBuilder EqualsAny(IReadOnlyCollection<string> values)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, false);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder EqualsAny(IReadOnlyCollection<string> values, bool ignoreCase)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, ignoreCase);
        AppendPredicate(matchPredicate);
        return this;
    }
    
    public InputParserPredicateBuilder EqualsAny(IReadOnlyCollection<string> values, bool ignoreCase, Repetition repetition)
    {
        var matchPredicate = new IsMatchingAnyStringPredicate(values, ignoreCase);
        var repetitionPredicate = new RepeatPredicate(matchPredicate, repetition);
        AppendPredicate(repetitionPredicate);
        return this;
    }

    public IParserInputPredicate Done()
    {
        return _predicate;
    }

    private void AppendPredicate(IParserInputPredicate predicate)
    {
        if (_predicate is PredicateConcatenation list)
        {
            list.Add(predicate);
        }
        else if (_predicate == Predicate.Empty)
        {
            _predicate = predicate;
        }
        else
        {
            var temp = new PredicateConcatenation(_predicate, predicate);
            _predicate = temp;
        }
    }
}

public struct Repetition
{
    public int Minimum { get; private set; }
    public int Maximum { get; private set; }

    public static Repetition ZeroOrMore = new Repetition
    {
        Minimum = 0,
        Maximum = -1
    };

    public static Repetition ZeroOrOne = new Repetition
    {
        Minimum = 0,
        Maximum = 1
    };

    public static Repetition OneOrMore = new Repetition
    {
        Minimum = 1,
        Maximum = -1
    };

    public static Repetition Exact(int count)
    {
        return new Repetition
        {
            Minimum = count,
            Maximum = count
        };
    }

    public static Repetition Range(int minimum, int maximum)
    {
        return new Repetition
        {
            Minimum = minimum,
            Maximum = maximum
        };
    }

}

public class Foo
{

    private Foo()
    {
        var builder = new InputParserPredicateBuilder();
        builder
            .Equals('1', Repetition.OneOrMore);
    }
}
