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
        public ArrowSprite(SpriteDefinition spriteDefinition, Arrow arrow) : base(spriteDefinition)
        {
            Rotation = RotationFromVelocity(arrow.Velocity);
            Layer = LayerConstants.Projectile;
            AttachedObject = arrow;
        }
    }
}
