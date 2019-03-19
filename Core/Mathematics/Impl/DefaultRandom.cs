using System;
using Core.Mathematics;

namespace Batronix.Core.Mathematics.Impl
{
    public class DefaultRandom : IRandom
    {
        private readonly Random _random = new Random();

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
    }
}
