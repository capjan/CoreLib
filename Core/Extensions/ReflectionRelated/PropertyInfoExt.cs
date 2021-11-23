using System;
using System.Reflection;
using Core.ControlFlow;

namespace Core.Extensions.ReflectionRelated
{
    public static class PropertyInfoExt
    {
        public static T GetAttribute<T>(this PropertyInfo info) where T : Attribute
        {
            var type = typeof(T);
            var result = info.GetCustomAttribute(type) as T;
            return result ?? throw new InvalidOperationException($"attribute {nameof(type.Name)} is not present");
        }

        public static bool TryGetAttribute<T>(this PropertyInfo info, out T attribute) where T : Attribute, new()
        {
            return new Tryify<T>().TryInvoke(info.GetAttribute<T>, out attribute, new T());
        }
    }
}
