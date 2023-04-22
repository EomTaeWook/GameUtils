using System;
using System.Collections.Generic;

namespace GameUtils
{
    public class RandomGenerator
    {
        private readonly Random _random = new Random();
        private int _used = 0;
        public RandomGenerator()
        {
            _random = new Random(DateTime.Now.Ticks.GetHashCode());
        }
        public RandomGenerator(int seed)
        {
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
            return _random.Next(min, max + 1);
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
        public static void Shuffle<T>(IList<T> items)
        {            
            int n = items.Count;
            var random = new Random();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                var value = items[k];
                items[k] = items[n];
                items[n] = value;
            }
        }
    }
}
