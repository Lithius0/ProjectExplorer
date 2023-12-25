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
    public class SwordExplosionSprite : SimpleAnimatedSprite
    {
        private static readonly Rectangle Source = new(96, 0, 8, 16);

        public SwordExplosionSprite(Texture2D texture, ISticky sticky, Vector2 velocity) : base(texture, Source, sticky, 4, 1/15f)
        {
            SpriteEffects flip = SpriteEffects.None;

            // Just imagine the |= operator as appying that effect to the flip variable
            // It's a bitwise or operator, FlipHorizontally is basically 01 while FlipVertically is 10. 
            // If both effects are neeed (X and Y > 0), they or together to make 11, which flips both directions
            if (velocity.X > 0)
                flip |= SpriteEffects.FlipHorizontally;
            if (velocity.Y > 0)
                flip |= SpriteEffects.FlipVertically;

            Layer = LayerConstants.Projectile;

            SpriteEffect = flip;
        }
    }
}
