using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.SpriteUtil.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectExplorer.Tiles.ControlTiles
{
    /// <summary>
    /// The slider element. The position and collision of this object is specifically the slider thumb.
    /// The slider markers are static.
    /// </summary>
    public class Slider : IGameObject, ICollidable, ISticky
    {
        protected static int Width = Tiling.ToPixels(3);
        private ISprite sliderSprite;
        private ISprite thumbSprite;
        private Vector2 minPosition;
        private Vector2 position;
        private ICollisionHandler handler;
        private TextTile display;
        private string displayFormat;
        protected ILevel level;
        protected float min;
        protected float max;
        public Vector2 Position => position;
        /// <summary>
        /// Value of the slider normalized on [0, 1]
        /// i.e. current value of the slider with 0 as the min and 1 as the max.
        /// </summary>
        public float NormalizedValue
        {
            get { return (position.X - minPosition.X) / Width; }
            set
            {
                SetPosition(MathHelper.Clamp(value, 0, 1) * Width + minPosition.X);
            }
        }
        public float Value
        {
            get { return MathHelper.Lerp(min, max, NormalizedValue); }
            set { NormalizedValue = (value - min) / (max - min); }
        }

        public CollisionGroup Group => CollisionGroup.Triggers;

        public event EventHandler<SliderValueArgs> Changed;

        public Slider(Vector2 position, float min = 0, float max = 1)
        {
            this.min = min;
            this.max = max;

            this.position = position + new Vector2(Width * 0.5f, 0);
            minPosition = position;

            sliderSprite = ControlsSpriteFactory.Instance.GetSliderSprite(position);
            thumbSprite = ControlsSpriteFactory.Instance.GetSliderThumbSprite(this);
            
            handler = new SliderCollisionHandler(this);

        }

        public Slider(Vector2 position, string displayFormat, float min = 0, float max = 1) : this(position, min, max)
        {
            this.displayFormat = displayFormat;
            display = new("DuskB2", position + new Vector2(Tiling.ToPixels(3), 0), string.Format(displayFormat, Value));
        }

        /// <summary>
        /// Sets the x-value in the position of the slider.
        /// Should only be called by the CollisionHandler
        /// </summary>
        public void SetPosition(float x)
        {
            float oldValue = Value;
            x = MathHelper.Clamp(x, minPosition.X, minPosition.X + Width);
            position = new Vector2(x, minPosition.Y); // Y value discarded. Slider is 1D.

            if (oldValue != Value)
            {
                Changed?.Invoke(this, new SliderValueArgs(oldValue, Value));
                // If display exists, displayFormat should also exist. No need for a null check on displayFormat.
                display?.SetText(string.Format(displayFormat, Value));
            }
        }

        public Rectangle GetCollider()
        {
            return Positioning.ConstructFromAnchorPoint(position, Tiling.HalfVertical, AnchorPoints.TopMiddle);
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return handler;
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
            if (display != null)
            {
                this.level.Register(display);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sliderSprite.Draw(gameTime, spriteBatch);
            thumbSprite.Draw(gameTime, spriteBatch);
        }
    }

    public class SliderValueArgs : EventArgs
    {
        public float OldValue { get; set; }
        public float NewValue { get; set; }

        public SliderValueArgs(float oldValue, float newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
