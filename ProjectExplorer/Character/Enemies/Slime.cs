using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProjectExplorer.Character.Enemies
{
    public class Slime : Enemy, IMitotic
    {
        private IAnimatedSprite rightSprite;
        private IAnimatedSprite leftSprite;
        private bool moving = false;
        private float moveTimer = 2;

        public Slime(Vector2 position)
        {
            this.position = position;
            rightSprite = new SimpleAnimatedSprite(SpriteManager.GetTexture("Slime"), new Rectangle(0, 0, 16, 16), this, 3, 1 / 6f, true);
            leftSprite = new SimpleAnimatedSprite(SpriteManager.GetTexture("Slime"), new Rectangle(0, 16, 16, 16), this, 3, 1 / 6f, true);
            direction = Direction.RIGHT;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Highly temporary slime AI
            if (moveTimer > 0)
            {
                moveTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                moveTimer = 4;
                moving = !moving;
                if (moving)
                {
                    rightSprite.Play();
                    leftSprite.Play();
                    direction = direction.Flip();
                }
                else
                {
                    rightSprite.Stop();
                    leftSprite.Stop();
                }
            }

            if (moving)
            {
                position += direction.GetVector2() * (float)gameTime.ElapsedGameTime.TotalSeconds * 5;
            }
            
        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            return new Slime(objectDefinition.Position);
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
