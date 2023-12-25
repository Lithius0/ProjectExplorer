using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    // Presets for Anchor points
    public static class AnchorPoints
    {
        public readonly static Vector2 TopLeft = new Vector2(0, 0);
        public readonly static Vector2 TopMiddle = new Vector2(0.5f, 0);
        public readonly static Vector2 TopRight = new Vector2(1, 0);
        public readonly static Vector2 MiddleLeft = new Vector2(0, 0.5f);
        public readonly static Vector2 Middle = new Vector2(0.5f, 0.5f);
        public readonly static Vector2 MiddleRight = new Vector2(1f, 0.5f);
        public readonly static Vector2 BottomLeft = new Vector2(0, 1);
        public readonly static Vector2 BottomMiddle = new Vector2(0.5f, 1);
        public readonly static Vector2 BottomRight = new Vector2(1, 1);
    }
}
