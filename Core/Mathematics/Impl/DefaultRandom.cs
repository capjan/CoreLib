using System;

namespace Core.Mathematics.Impl
{
    public class DefaultRandom : IRandom
    {
        public DefaultRandom()
        {
            _random = new Random();    
        }

        public DefaultRandom(int seed)
        {
            _random = new Random(seed);    
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        private readonly Random _random;
    }
}
