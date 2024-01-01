using ProjectExplorer.Projectiles;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Sprites
{
    public class ArrowSprite : BaseSprite
    {
        private static readonly Rectangle Source = new(0, 0, 16, 16);

        public ArrowSprite(Texture2D texture, Arrow arrow) : base(texture, Source)
        {
            Rotation = RotationFromVelocity(arrow.Velocity);
            Layer = LayerConstants.Projectile;
            AttachedObject = arrow;
        }
    }
}
