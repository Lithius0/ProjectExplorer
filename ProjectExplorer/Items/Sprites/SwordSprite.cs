using ProjectExplorer.Projectiles;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Sprites
{
    public class SwordSprite : SimpleAnimatedSprite
    {
        protected static readonly Rectangle Source = new(0, 0, 16, 16);
        public SwordSprite(Texture2D texture, Projectile sword) : base(texture, Source, sword, 4, 1/15f)
        {
            Rotation = RotationFromVelocity(sword.Velocity);
            Layer = LayerConstants.Projectile;
        }
    }
}
