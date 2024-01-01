using ProjectExplorer.Collision;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Levels;
using ProjectExplorer.Character;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Tiles;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Projectiles
{
    /// <summary>
    /// General projectile class. Manages damage and collision.
    /// For the most part, subclasses should implement the motion.
    /// This class only has basic linear motion.
    /// </summary>
    public abstract class Projectile : IGameObject, ICollidable
    {
        protected int damage;
        protected Point size = Tiling.Full;
        protected Vector2 velocity = Vector2.Zero;
        protected ICollisionHandler collisionHandler;
        protected ICharacter owner;
        protected ISprite sprite;
        // If true, the projectile will be removed after it's been drawn rather than immediately.
        protected bool removeAfterDraw = false;

        private bool removed = false;
        
        public Vector2 Position { get; set; }
        public ICharacter Owner
        { get { return owner; } }
        public Vector2 Velocity
        { get { return velocity; } }
        public int Damage
        { get { return damage; } }

        public CollisionGroup Group => CollisionGroup.Projectiles;

        protected int id;
        protected ILevel manager;

        protected Projectile(Vector2 position, Vector2 velocity, ICharacter owner, int damage = 1)
        {
            this.damage = damage;
            Position = position;
            this.velocity = velocity;
            this.owner = owner;
            collisionHandler = new ProjectileCollisionHandler(this);
        }

        protected Projectile(ICharacter owner, float speed, int damage = 1)
        {
            this.damage = damage;
            Position = owner.Position + owner.Direction.ToVector2() * Tiling.ToPixels(0.5f);
            velocity = owner.Direction.ToVector2() * speed;
            this.owner = owner;
            collisionHandler = new ProjectileCollisionHandler(this);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite?.Draw(gameTime, spriteBatch);
            if (removed)
                manager?.Deregister(this);
        }

        public virtual Rectangle GetCollider()
        {
            return AnchorPoints.Construct(Position, Tiling.Full, AnchorPoints.Middle);
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return collisionHandler;
        }

        public void OnRegister(ILevel manager)
        {
            this.manager = manager;
        }

        public virtual void Update(GameTime gameTime)
        {
            // General projectile physics, override as necessary
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Removes the projectile from its object manager.
        /// </summary>
        public virtual void Remove()
        {
            if (removeAfterDraw)
            {
                removed = true;
            } 
            else
            { 
                manager?.Deregister(this);
            }
        }
    }
}
