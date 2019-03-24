using System.Collections.Generic;

namespace Core.Text.Formatter
{
    public interface ISeparatorFormatter<in T> : ITextFormatter<IEnumerable<T>>
    {
    }
}
