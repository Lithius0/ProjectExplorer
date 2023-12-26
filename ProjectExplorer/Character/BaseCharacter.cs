using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character
{
    public abstract class BaseCharacter : ICharacter, ICollidable, ISticky
    {
        protected Vector2 position;
        protected Point size = Tiling.Full;
        protected Direction direction;

        protected float health;
        protected float maxHealth = 6;

        protected float invincibleDuration = 0.75f;
        protected float invincibleTimer = 0;

        protected float pushDuration = 0.1f;
        private float pushTimer = 0;
        protected Vector2 pushVelocity = Vector2.Zero;

        protected ILevel level;
        protected CharacterExclusion exclusion;

        public event EventHandler Died;

        public ILevel Level { get { return level; } }
        public bool RecentlyDamaged { get { return invincibleTimer > 0; } }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Direction Direction
        { 
            get { return direction; } 
            set { direction = value; }
        }

        public float Health
        {
            get { return health; }
            set
            {
                health = Math.Clamp(value, 0, maxHealth);
                if (health <= 0)
                    Kill();
            }       
        }

        public float MaxHealth
        { 
            get { return maxHealth; } 
            set { maxHealth = Math.Max(value, 0); }
        }

        public abstract CollisionGroup Group { get; }

        public BaseCharacter()
        {
            exclusion = new CharacterExclusion(this);
        }
        public virtual bool Damage(int damage, Vector2 push)
        {
            if (!RecentlyDamaged)
            {
                Health -= damage;
                invincibleTimer = invincibleDuration;
                // Translating push distance to speed.
                pushVelocity = push / pushDuration;
                pushTimer = pushDuration;
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void Kill()
        {
            health = 0; // Don't use the property. Don't want to trigger kill again.
            Died?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public Rectangle GetTransform()
        {
            return Positioning.ConstructFromAnchorPoint(position, size, AnchorPoints.Middle);
        }
        public virtual void Heal(int heal)
        {
            Health += heal;
        }
        public virtual void Move(Vector2 move)
        {
            position += move;
        }
        public virtual void OnRegister(ILevel level)
        {
            this.level = level;
            level.Register(exclusion);
        }
        public virtual void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (RecentlyDamaged)
            {
                invincibleTimer -= elapsedTime;
            }
            if (pushTimer > 0)
            {
                pushTimer -= elapsedTime;


                Rectangle target = exclusion.GetCollider();
                // Position of character is not the same as position for exclusion, so these must be reconciled.
                Vector2 offset = target.Location.ToVector2() - position;
                target.Offset(pushVelocity * elapsedTime);

                position = level.LastValidSpotBetween(exclusion, target).Location.ToVector2() - offset;
            }
        }

        public virtual Rectangle GetCollider()
        {
            return Positioning.ConstructFromAnchorPoint(position, size, AnchorPoints.Middle);
        }

        public abstract ICollisionHandler GetCollisionHandler();
    }
}
