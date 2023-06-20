using System;

namespace GameUtils.Path
{
    public class PathNode : IEquatable<PathNode>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool Equals(PathNode other)
        {
            return X == other.X && Y == other.Y;
        }

        public PathNode(int x, int y) 
        {
            this.X = x;
            this.Y = y;
        }
    }    
}
