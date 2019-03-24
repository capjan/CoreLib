using System;
using System.Linq;
using System.Numerics;

namespace Core.Generic
{
    /// <summary>
    /// Makes it easy to check if a generic type is part of a given type list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeChecker<T>
    {

        /// <summary>
        /// Creates a type checker to check if the generic type is a number
        /// </summary>        
        public static TypeChecker<T> Numeric()
        {
            return new TypeChecker<T>( typeof(byte), typeof(sbyte),
                                              typeof(short), typeof(ushort),
                                              typeof(int), typeof(uint),
                                              typeof(long), typeof(ulong),
                                              typeof(float),
                                              typeof(double),
                                              typeof(decimal),
                                              typeof(BigInteger));
        }

        public TypeChecker(params Type[] types)
        {
            _types = types;
        }

        public bool IsValid()
        {
            var type = typeof(T);
            return _types.Contains(type);
        }

        private readonly Type[] _types;
    }
}
