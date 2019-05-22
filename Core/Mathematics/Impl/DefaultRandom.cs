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
            if (maxValue == int.MaxValue) maxValue -= 1;
            return _random.Next(maxValue + 1);
        }

        public int Next(int minValue, int maxValue)
        {
            if (maxValue == int.MaxValue) maxValue -= 1;
            return _random.Next(minValue, maxValue + 1);
        }

        private readonly Random _random;
    }
}
