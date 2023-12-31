using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.Character.Sprite;
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
        private IAnimatedSprite sprite;
        private bool moving = false;
        private float moveTimer = 2;

        public Slime(Vector2 position)
        {
            this.position = position;
            direction = Direction.RIGHT;
            sprite = new SlimeSprite(this);
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
                    sprite.Play();
                    direction = direction.Flip();
                }
                else
                {
                    sprite.Stop();
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
            sprite.Draw(gameTime, spriteBatch);
        }
    }
}
