using System;
using System.Collections.Generic;

namespace Utils
{
    public class RandomGenerator
    {
        private readonly Random _random = new Random();
        private int _used = 0;
        public int RandomSeed { get; private set; }
        public RandomGenerator()
        {
            RandomSeed = DateTime.Now.Ticks.GetHashCode();
            _random = new Random(RandomSeed);
        }
        public RandomGenerator(int seed)
        {
            RandomSeed = seed;
            _random = new Random(seed);
        }
        public int Next()
        {
            _used++;
            return _random.Next();
        }
        public int Next(int min, int max)
        {
            if (max >= int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(max));
            }
            _used++;
            return _random.Next(min, max);
        }
        public int Next(int max)
        {
            _used++;
            return Next(0, max);
        }

        public double NextDouble()
        {
            _used++;
            return _random.NextDouble();
        }

        public int GetUsedIndex()
        {
            return _used;
        }
        public List<T> Shuffle<T>(IList<T> items)
        {
            var dest = new List<T>(items);
            int n = dest.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                var value = dest[k];
                dest[k] = dest[n];
                dest[n] = value;
            }
            return dest;
        }
    }
}
