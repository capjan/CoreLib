﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Enums
{
    /// <summary>
    /// Provides Utility Functions for Enumerations
    /// </summary>
    public class EnumUtil
    {
        /// <summary>
        /// Lists all Entries of the given enumeration as KeyValuePairs
        /// </summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <returns></returns>
        public static IEnumerable<EnumInfo<T>> List<T>()
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(enumValue => {
                            var enumName = Enum.GetName(typeof(T), enumValue);
                            return new EnumInfo<T>(enumName, enumValue);
                        });
        }
    }

    /// <summary>
    /// Information about an enumeration entry
    /// </summary>
    /// <typeparam name="T">Type of the enumeration</typeparam>
    public readonly struct EnumInfo<T>
    {
        /// <summary>
        /// Name of the Enum Value
        /// </summary>
        public string Name  { get; }
        
        /// <summary>
        /// Value
        /// </summary>
        public T      Value { get; }

        public EnumInfo(string name, T value)
        {
            Name  = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }



}
