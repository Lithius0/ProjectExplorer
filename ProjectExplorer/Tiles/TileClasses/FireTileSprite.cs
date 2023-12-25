using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;

namespace ProjectExplorer.Tiles.TileClasses
{
    public class FireTileSprite : ISprite
    {
        protected Vector2 position;
        protected int width, height;

        protected Texture2D texture;
        protected Rectangle source;
        protected Rectangle destination;

        private bool fireState = false;
        private int fireAnimTimer = 0;
        private int fireAnimCooldown = 500;

        public FireTileSprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            width = 16;
            height = 16;
            source = new Rectangle(48, 64, width, height);
            destination = new Rectangle(position.ToPoint(), Tiling.Full);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            fireAnimTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (fireAnimTimer > fireAnimCooldown)
            {
                fireAnimTimer -= fireAnimCooldown;

                fireState = !fireState;
            }

            if (fireState)
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0, Vector2.Zero, SpriteEffects.None, .2f);
            }
            else
            {
                spriteBatch.Draw(texture, destination, source, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, .2f);
            }
        }
    }
}
