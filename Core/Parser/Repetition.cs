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