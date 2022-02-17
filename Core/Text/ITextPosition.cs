using System;

namespace Core.Text;

public interface ITextPosition : IEquatable<ITextPosition>
{
    int LineNumber { get; }
    int ColumnNumber { get; }
}