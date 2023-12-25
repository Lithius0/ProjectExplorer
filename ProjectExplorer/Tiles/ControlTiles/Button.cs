using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.Character;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    public class Button : IGameObject, ICollidable
    {
        private ISprite sprite;
        protected bool on = false;
        protected bool pressed = false;
        protected ICollisionHandler handler;
        protected ILevel level;
        public virtual bool On => on;

        private Rectangle collider;

        public CollisionGroup Group => CollisionGroup.Triggers;

        public EventHandler PressStart;
        public EventHandler PressEnd;

        public Button(Vector2 position)
        {
            collider = new Rectangle(position.ToPoint(), Tiling.Full);
            collider.Inflate(-2, -2); // Shrink a collider just a bit to be harder to hit
            handler = new ButtonCollisionHandler(this);
            sprite = ControlsSpriteFactory.Instance.GetButtonSprite(position, this);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }

        public Rectangle GetCollider()
        {
            return collider;
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return handler;
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (on && !pressed)
            {
                PressEnd?.Invoke(this, EventArgs.Empty);
            }
            else if (!on && pressed)
            {
                PressStart?.Invoke(this, EventArgs.Empty);
            }
            on = pressed;
            // Collision handler will call press every frame that the player is colliding with button.
            pressed = false;
        }

        public void Press()
        {
            pressed = true;
        }
    }
}
