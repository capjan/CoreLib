using System;

namespace Core.Text.Impl
{
    public class TextPosition : ITextPosition
    {
        public TextPosition(int lineNumber = 1, int columnNumber = 1)
        {
            if (lineNumber <= 0) throw new ArgumentException("line number must be 1 or greater");
            if (columnNumber <= 0) throw new ArgumentException("column number must be 1 or greater");

            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
        }

        public int LineNumber { get; } 
        public int ColumnNumber { get; }

        protected bool Equals(TextPosition other)
        {
            return LineNumber == other.LineNumber && ColumnNumber == other.ColumnNumber;
        }

        public bool Equals(ITextPosition other)
        {
            return Equals((object) other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TextPosition) obj);
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
