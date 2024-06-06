using System.Numerics;

namespace Utils
{
    public struct Coordinate2
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public Coordinate2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public static Coordinate2 Zero => default;

        public static double Distance(Coordinate2 a, Coordinate2 b)
        {
            return System.Math.Sqrt(System.Math.Pow(a.X - b.X, 2) + System.Math.Pow(a.Y - b.Y, 2));
        }
        public static Coordinate2 operator +(Coordinate2 a, Coordinate2 b)
        {
            return new Coordinate2(a.X + b.X, a.Y + b.Y);
        }
        public static Coordinate2 operator -(Coordinate2 a, Coordinate2 b)
        {
            return new Coordinate2(a.X - b.X, a.Y - b.Y);
        }
        public static explicit operator Coordinate2(Vector2 vector)
        {
            return new Coordinate2(vector.X, vector.Y);
        }
        public static explicit operator Vector2(Coordinate2 coordinate)
        {
            return new Vector2(coordinate.X, coordinate.Y);
        }
    }
}
