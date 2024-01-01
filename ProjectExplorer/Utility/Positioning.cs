using Microsoft.Xna.Framework;
using System;

namespace ProjectExplorer.Utility
{
    public static class Positioning
    {
        public static Vector2 GetCenter(Vector2 topLeft, Vector2 size)
        {
            return topLeft + (size / 2);
        }
        public static Vector2 GetCenter(Vector2 topLeft, Point size)
        {
            return topLeft + (size.ToVector2() / 2);
        }

        /// <summary>
        /// Linear interpolation between points.
        /// </summary>
        public static Point Lerp(Point start, Point end, float alpha)
        {
            return Vector2.Lerp(start.ToVector2(), end.ToVector2(), alpha).ToPoint();
        }

        /// <summary>
        /// Linear interpolation between two rectangles. Also lerps size.
        /// </summary>
        public static Rectangle Lerp(Rectangle start, Rectangle end, float alpha)
        {
            return new Rectangle(Lerp(start.Location, end.Location, alpha), Lerp(start.Size, end.Size, alpha));
        }

        /// <summary>
        /// Rotates the given vector about its tail by the given angle
        /// </summary>
        public static Vector2 Rotate(Vector2 vector, float angle)
        {
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);

            return new Vector2(vector.X * cos - vector.Y * sin, vector.X * sin + vector.Y * cos);
        }
    }
}
