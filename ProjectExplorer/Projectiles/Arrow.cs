using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Projectiles.Sprites;
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

namespace ProjectExplorer.Projectiles
{
    public class Arrow : Projectile
    {
        private float lifetime;

        public Arrow(Vector2 position, Vector2 velocity, ICharacter owner, float lifetime) : base(position, velocity, owner)
        {
            // Changing the hitbox depending on whether the arrow is flying horizontally or vertically.
            if (Math.Abs(velocity.Y) > Math.Abs(velocity.X))
            {
                size = Tiling.HalfVertical;
            }
            else
            {
                size = Tiling.HalfHorizontal;
            }

            removeAfterDraw = true;
            this.lifetime = lifetime;
            sprite = ProjectileSpriteFactory.Instance.GetArrowSprite(this);
        }

        public override void Update(GameTime gameTime)
        {
            lifetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (lifetime < 0)
            {
                Remove();
            }
            base.Update(gameTime);
        }
    }
}
