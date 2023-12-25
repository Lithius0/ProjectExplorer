using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    /// <summary>
    /// An implementation of ISprite which holds the core functionality of most sprites.
    /// Extend from this class if something more advanced behavior is needed.
    /// </summary>
    public class BaseSprite : ISprite, IStickable
    {
        protected Texture2D texture;
        protected Rectangle source;
        protected ISticky stuck = null;

        public Vector2 Position { get; set; }
        // The relative "origin" for positioning, scaling, and rotating the sprite. Use the AnchorPoints class for most purposes.
        public Vector2 AnchorPoint { get; set; } = AnchorPoints.Middle;
        public float Scale { get; set; } = 1f;
        public float Rotation { get; set; } = 0f; // Radians
        public float Layer { get; set; } = 0.1f;
        public Color Color { get; set; } = Color.White;
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;

        public BaseSprite(Texture2D texture, Rectangle source, Vector2 position)
        {
            this.texture = texture;
            this.source = source;
            Position = position;
        }
        public BaseSprite(Texture2D texture, Rectangle source, ISticky sticky)
        {
            this.texture = texture;
            this.source = source;
            StickTo(sticky);
        }

        // This constructor should only be used with classes which do not have a static source. (i.e. animated classes)
        // For this reason it's also protected, too easy to throw a null-related exception.
        protected BaseSprite()
        {

        }

        public static BaseSprite GetMissingSprite(Vector2 position)
        { 
            return new BaseSprite(SpriteManager.GetTexture("Missing"), new Rectangle(0, 0, 16, 16), position); 
        }

        public static float RotationFromVelocity(Vector2 velocity)
        {
            return (float)Math.Atan2(velocity.Y, velocity.X);
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Vector2 position = Position;
            if (stuck != null)
            {
                position = stuck.Position;
            }

            Vector2 origin = source.Size.ToVector2() * AnchorPoint;
            // Position here uses Vector2 for more precise math.
            // The Floor call is to make sure the sprite lines up with the collider, which is a rectangle made of ints.
            spriteBatch.Draw(texture, Vector2.Floor(position), source, Color, Rotation, origin, Scale, SpriteEffect, Layer);
        }

        public void StickTo(ISticky stickyObject)
        {
            stuck = stickyObject;
        }
    }
}
