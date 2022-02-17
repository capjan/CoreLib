using System.Collections.Generic;
using Core.Extensions.TextRelated;
using Core.Text.Formatter;

// ReSharper disable UnusedMember.Global

namespace Core.Extensions.CollectionRelated;

public static class EnumerableExt
{
    public static string ToSeparatedString<T>(
        this IEnumerable<T> value,
        string separator = ", ",
        int groupLength = 1,
        ITextFormatter<T>? itemFormatter = default,
        string nullPlaceholder = "")
    {
        var formatter = new SeparatorFormatter<T>(
            separator,
            groupLength,
            itemFormatter,
            nullPlaceholder
        );
        return formatter.WriteToString(value);
    }
}