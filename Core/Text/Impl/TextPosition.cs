using System;
using System.Net.Http.Headers;

namespace Core.Text.Impl;

public class TextPosition : ITextPosition, IEquatable<TextPosition>
{
    private class EmptyTextPosition : ITextPosition
    {
        public bool Equals(ITextPosition? other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return LineNumber == other.LineNumber && ColumnNumber == other.ColumnNumber;
        }

        public int LineNumber => 0;
        public int ColumnNumber => 0;
    }

    public static ITextPosition Empty = new EmptyTextPosition();
    public static ITextPosition Start = new TextPosition(1,1);

    public TextPosition(int lineNumber, int columnNumber)
    {
        if (lineNumber <= 0) throw new ArgumentException("line number must be 1 or greater");
        if (columnNumber <= 0) throw new ArgumentException("column number must be 1 or greater");

        LineNumber = lineNumber;
        ColumnNumber = columnNumber;
    }

    public int LineNumber { get; }
    public int ColumnNumber { get; }


    public bool Equals(ITextPosition? other)
    {
        if (other == null) return false;
        return ColumnNumber == other.ColumnNumber && LineNumber == other.LineNumber;
    }

    public override int GetHashCode()
    {
        return new {LineNumber, ColumnNumber}.GetHashCode();
    }

    public bool Equals(TextPosition? other)
    {
        if (other == null) return false;
        return LineNumber == other.LineNumber && ColumnNumber == other.ColumnNumber;
    }

    public override bool Equals(object? obj)
    {
        return obj is TextPosition other && Equals(other);
    }

    public new string ToString()
    {
        return $"Line: {LineNumber}, Column: {ColumnNumber}";
    }
}