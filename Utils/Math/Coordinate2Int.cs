using System.Numerics;

namespace GameUtils.Math
{
    public struct Coordinate2Int
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Coordinate2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Coordinate2Int Zero => default;
        public static double Distance(Coordinate2Int a, Coordinate2Int b)
        {
            return System.Math.Sqrt(System.Math.Pow(a.X - b.X, 2) + System.Math.Pow(a.Y - b.Y, 2));
        }
        public static Coordinate2Int operator +(Coordinate2Int a, Coordinate2Int b)
        {
            return new Coordinate2Int(a.X + b.X, a.Y + b.Y);
        }
        public static Coordinate2Int operator -(Coordinate2Int a, Coordinate2Int b)
        {
            return new Coordinate2Int(a.X - b.X, a.Y - b.Y);
        }
        public static explicit operator Coordinate2Int(Vector2 vector)
        {
            return new Coordinate2Int((int)vector.X, (int)vector.Y);
        }
        public static explicit operator Vector2(Coordinate2Int coordinate)
        {
            return new Vector2(coordinate.X, coordinate.Y);
        }
    }
}
