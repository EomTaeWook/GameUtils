using System.Threading;

namespace Utils
{
    public class IntIncrementer
    {
        private int _index;
        public int Increment()
        {
            Interlocked.Increment(ref _index);
            return _index;
        }
        public int Current()
        {
            return _index;
        }
    }
}