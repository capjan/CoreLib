using System;

namespace Core.Logging.Logger
{
    public static class Logger
    {
        public static ILogger Create(string typeFullName)
        {
            return new TypeLogger(typeFullName);
        }

        public static ILogger Create(Type type)
        {
            var className = type.FullName;
            return new TypeLogger(className);
        }

        public static ILogger Create<T>() where T : class
        {
            return Create(typeof(T));
        }
    }
}
