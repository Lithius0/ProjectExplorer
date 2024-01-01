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

namespace ProjectExplorer.Projectiles.Sprites
{
    public class SwordSweepSprite : BaseSprite
    {
        protected static readonly Rectangle Source = new(0, 16, 16, 16);
        protected SwordSweep sword;
        public SwordSweepSprite(SwordSweep sword) : base(SpriteManager.GetTexture("Items"), Source)
        {
            this.sword = sword;
            Rotation = RotationFromVelocity(sword.Velocity);
            Layer = LayerConstants.Projectile;
            AttachedObject = sword;
        }
        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Rotation = RotationFromVelocity(sword.Velocity) + sword.Angle + (float)(Math.PI / 4);
            base.Draw(gametime, spriteBatch);
        }
    }
}
