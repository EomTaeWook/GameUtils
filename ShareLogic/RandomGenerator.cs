using System;

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
    }
}
