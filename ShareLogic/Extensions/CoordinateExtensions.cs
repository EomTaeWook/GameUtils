using System;
using System.Numerics;

namespace GameUtils.Extensions
{
    public enum Direction
    {
        Left, Right, Up, Down
    }

    public static class CoordinateExtensions
    {
        public static Vector2 Lerp(this Vector2 value1, Vector2 value2, float amount)
        {
            return new Vector2(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount);
        }
        public static Coordinate2 Lerp(this Coordinate2Int value1, Coordinate2Int value2, float amount)
        {
            int x = (int)(value1.X + (value2.X - value1.X) * amount);
            int y = (int)(value1.Y + (value2.Y - value1.Y) * amount);
            return new Coordinate2(x, y);
        }
        public static Coordinate2 Lerp(this Coordinate2 value1, Coordinate2 value2, float amount)
        {
            var x = value1.X + (value2.X - value1.X) * amount;
            var y = value1.Y + (value2.Y - value1.Y) * amount;
            return new Coordinate2(x, y);
        }
        public static float GridDistance(this Vector2 point1, Vector2 point2)
        {
            float dx = Math.Abs(point2.X - point1.X);
            float dy = Math.Abs(point2.Y - point1.Y);
            return Math.Max(dx, dy);
        }
        public static float GridDistance(this Coordinate2 point1, Coordinate2 point2)
        {
            float dx = Math.Abs(point2.X - point1.X);
            float dy = Math.Abs(point2.Y - point1.Y);
            return Math.Max(dx, dy);
        }
        public static int GridDistance(this Coordinate2Int point1, Coordinate2Int point2)
        {
            int dx = Math.Abs(point2.X - point1.X);
            int dy = Math.Abs(point2.Y - point1.Y);
            return Math.Max(dx, dy);
        }
        public static Direction CalculateDirection(this Coordinate2Int from, Coordinate2Int to)
        {
            return DetermineDirection(from.X, from.Y, to.X, to.Y);
        }
        public static Direction CalculateDirection(this Vector2 from, Vector2 to)
        {
            return DetermineDirection(from.X, from.Y, to.X, to.Y);
        }

        public static Direction CalculateDirection(this Coordinate2 from, Coordinate2 to)
        {
            return DetermineDirection(from.X, from.Y, to.X, to.Y);
        }
        private static Direction DetermineDirection(double fromX, double fromY, double toX, double toY)
        {
            double diffX = toX - fromX;
            double diffY = toY - fromY;

            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                return diffX > 0 ? Direction.Right : Direction.Left;
            }
            else
            {
                return diffY > 0 ? Direction.Up : Direction.Down;
            }
        }

    }
}
