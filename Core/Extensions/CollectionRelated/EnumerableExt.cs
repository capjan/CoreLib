using System;
using System.Collections.Generic;
using Core.Extensions.TextRelated;
using Core.Text.Formatter.Impl;
// ReSharper disable UnusedMember.Global

namespace Core.Extensions.CollectionRelated
{
    public static class EnumerableExt
    {
        public static string ToSeparatedString<T>(
            this IEnumerable<T> value,
            string separator = ", ",
            int groupLength = 1,
            Func<T, string> toStringFunc = default,
            string nullPlaceholder = "")
        {
            var formatter = new DefaultSeparatorFormatter<T>(
                separator,
                groupLength,
                toStringFunc,
                nullPlaceholder
            );
            return formatter.WriteToString(value);
        }
    }
}
