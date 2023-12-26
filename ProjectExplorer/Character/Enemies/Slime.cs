using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.SpriteUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Enemies
{
    public class Slime : Enemy
    {
        private IAnimatedSprite rightSprite;
        private IAnimatedSprite leftSprite;

        public Slime()
        {
            rightSprite = new SimpleAnimatedSprite(SpriteManager.GetTexture("Slime"), new Rectangle(0, 0, 16, 16), this, 3, 1 / 15f, true);
            leftSprite = new SimpleAnimatedSprite(SpriteManager.GetTexture("Slime"), new Rectangle(0, 16, 16, 16), this, 3, 1 / 15f, true);
            direction = Direction.RIGHT;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (direction == Direction.RIGHT)
            {
                rightSprite.Draw(gameTime, spriteBatch);
            }
            else
            {
                leftSprite.Draw(gameTime, spriteBatch);
            }
        }
    }
}
