using ProjectExplorer.Collision;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Projectiles.Sprites;

namespace ProjectExplorer.Items
{
    public class Explosion : IGameObject, ICollidable
    {
        private ILevel level;
        private float lifetime = 7/10f;
        private float damageTime = 0.1f;
        private float age = 0;
        private IAnimatedSprite sprite;
        private ICollisionHandler collisionHandler;
        public Vector2 Position { get; set; }

        public Explosion(Vector2 position)
        {
            Position = position;
            sprite = new ExplosionSprite(position).Play();
            collisionHandler = new ExplosionCollision();
        }

        public CollisionGroup Group => CollisionGroup.Items;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }

        public Rectangle GetCollider()
        {
            return Positioning.ConstructFromAnchorPoint(Position, Tiling.Full, AnchorPoints.Middle);
        }

        public ICollisionHandler GetCollisionHandler()
        {
            if (age <= damageTime)
            {
                return collisionHandler;
            }
            else
            {
                return EmptyCollisionHandler.Instance;
            }
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public void Update(GameTime gameTime)
        {
            age += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (age > lifetime)
                level.Deregister(this);
        }
    }
}
