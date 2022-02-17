using System;
using System.Linq;

namespace Core.Extensions.GenericsRelated;

public static class GenericExt
{
    public static bool In<T>(this T value, params T[] values) where T : IEquatable<T>
    {
        return values.Any(i => i.Equals(value));
    }
}