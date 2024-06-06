using System;

namespace GameUtils.Path
{
    internal sealed class AStartNode : PathNode, IEquatable<AStartNode>, IComparable<AStartNode>
    {
        public int G { get; set; }
        public int H { get; set; }

        public int F { get => G + H; }

        public AStartNode Parent { get; set; }

        public AStartNode(int x, int y) : base(x, y)
        {
        }
        public int CompareTo(AStartNode other)
        {
            int compare = F.CompareTo(other.F);
            if (compare == 0)
            {
                compare = X.CompareTo(other.X);
                if (compare == 0)
                {
                    compare = Y.CompareTo(other.Y);
                }
            }
            return compare;
        }

        public bool Equals(AStartNode other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
