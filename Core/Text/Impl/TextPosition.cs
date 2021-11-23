using System;
using System.Net.Http.Headers;

namespace Core.Text.Impl
{


    public readonly struct TextPosition : ITextPosition
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
        public static ITextPosition Start = new TextPosition();

        public TextPosition(int lineNumber = 1, int columnNumber = 1)
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
            unchecked
            {
                return (LineNumber * 397) ^ ColumnNumber;
            }
        }
    }
}
