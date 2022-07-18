using System;
using System.Collections.Generic;


namespace Core.Parser;

public interface IPredicateBuilder
{
    IParserInputPredicate Predicate { get; }
    IPredicateBuilder Assert(Action<IPredicateBuilder> block);
    
    // char / char sets
    IPredicateBuilder Equals(char character);
    IPredicateBuilder Equals(char character, Repetition repetition);
    IPredicateBuilder Equals(char[] characterSet);
    IPredicateBuilder Equals(char[] characterSet, Repetition repetition);
    
    // string
    IPredicateBuilder Equals(string value);
    IPredicateBuilder Equals(string value, bool ignoreCase);
    IPredicateBuilder Equals(string value, Repetition repetition);
    IPredicateBuilder Equals(string value, bool ignoreCase, Repetition repetition);

    // not char / not char set
    IPredicateBuilder EqualsNot(char character);
    IPredicateBuilder EqualsNot(char character, Repetition repetition);
    IPredicateBuilder EqualsNot(char[] characterSet);
    IPredicateBuilder EqualsNot(char[] characterSet, Repetition repetition);
    IPredicateBuilder EqualsCharacterRange(char lowerBound, char upperBound);
    IPredicateBuilder EqualsCharacterRange(char lowerBound, char upperBound, Repetition repetition);
    IPredicateBuilder EqualsAny(params string[] values);
    IPredicateBuilder EqualsAny(IReadOnlyCollection<string> values);
    IPredicateBuilder EqualsAny(IReadOnlyCollection<string> values, bool ignoreCase);
    IPredicateBuilder EqualsAny(IReadOnlyCollection<string> values, bool ignoreCase, Repetition repetition);

    // Word Boundary
    /// <summary>
    /// Matches if the input stream is on a word boundary (last char is alphanumeric and the next is not or otherwise)
    /// </summary>
    /// <returns></returns>
    IPredicateBuilder EqualsWordBoundary();

    // logical or
    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder> logicalOrBuilder);

    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder);

    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder);

    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder);
    
    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition);

    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition);

    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition);

    IPredicateBuilder EqualsAny(
        Action<IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder, IPredicateBuilder> logicalOrBuilder, Repetition repetition);
}