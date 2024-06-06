using System.Threading;

namespace GameUtils
{
    public class LongIncrementer
    {
        private long _index;
        public long Increment()
        {
            Interlocked.Increment(ref _index);
            return _index;
        }
        public long Current()
        {
            return _index;
        }
    }
}