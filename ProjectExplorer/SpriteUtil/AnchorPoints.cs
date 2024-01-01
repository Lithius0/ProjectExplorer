using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    // Presets for Anchor points
    public static class AnchorPoints
    {
        public readonly static Vector2 TopLeft = new(0, 0);
        public readonly static Vector2 TopMiddle = new(0.5f, 0);
        public readonly static Vector2 TopRight = new(1, 0);
        public readonly static Vector2 MiddleLeft = new(0, 0.5f);
        public readonly static Vector2 Middle = new(0.5f, 0.5f);
        public readonly static Vector2 MiddleRight = new(1f, 0.5f);
        public readonly static Vector2 BottomLeft = new(0, 1);
        public readonly static Vector2 BottomMiddle = new(0.5f, 1);
        public readonly static Vector2 BottomRight = new(1, 1);

        /// <summary>
        /// Create a rectangle such that the origin given will lie at the anchor point of the new rectangle.
        /// <br />
        /// For example, calling this method with the origin (25, 62) and the BottomLeft anchor point will
        /// create a rectangle with its bottom middle at the position (25, 62)
        /// </summary>
        public static Rectangle Construct(Vector2 origin, Point size, Vector2 anchorPoint)
        {
            origin -= size.ToVector2() * anchorPoint;
            return new Rectangle(origin.ToPoint(), size);
        }

        /// <summary>
        /// Create a rectangle such that the origin given will lie at the anchor point of the new rectangle.
        /// <br />
        /// For example, calling this method with the origin (25, 62) and the BottomLeft anchor point will
        /// create a rectangle with its bottom middle at the position (25, 62)
        /// </summary>
        public static Rectangle Construct(Point origin, Point size, Vector2 anchorPoint)
        {
            origin -= (size.ToVector2() * anchorPoint).ToPoint();
            return new Rectangle(origin, size);
        }
    }
}
