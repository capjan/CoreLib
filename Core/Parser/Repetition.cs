using System;

namespace Core.Parser;

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
        if (count <= 0) throw new ArgumentException($"{nameof(count)} ({count}) must be > 0");
        return new Repetition
        {
            Minimum = count,
            Maximum = count
        };
    }

    public static Repetition Range(int minimum, int maximum)
    {
        if (minimum < 0) throw new ArgumentException($"{nameof(minimum)} ({minimum}) must be >= 0");
        if (minimum > maximum)
            throw new ArgumentException($"maximum {maximum} must be greater that minimum ({minimum}) value",
                nameof(maximum));
        return new Repetition
        {
            Minimum = minimum,
            Maximum = maximum
        };
    }
}