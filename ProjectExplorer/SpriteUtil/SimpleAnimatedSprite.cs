using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    /// <summary>
    /// Generalized class for basic animations.
    /// Requires a specialized spritesheet.
    /// Animation frames must be placed horizontally, without gaps, and in order from left to right.
    /// </summary>
    public class SimpleAnimatedSprite : BaseSprite, IAnimatedSprite
    {
        protected Rectangle startSource;
        protected int frames; // Number of frames to animated
        protected bool repeat = true; // If true, animation will repeat
        protected float delay = 0.03f; // Time delay between frames (seconds)
        protected double age = 0; // Time since last frame

        public event EventHandler OnPlay;
        public event EventHandler OnStop;
        public event EventHandler OnPause;
        public event EventHandler ReachedEnd;

        protected int NextFrame // The logical next frame.
        {
            get { return (int)(age / delay); }
        }
        protected int NextValidFrame // The next frame that is actually going to be displayed.
        {
            get { return Math.Clamp(NextFrame, 0, frames - 1); }
        }
        protected bool playing = false;


        public SimpleAnimatedSprite(Texture2D texture, Rectangle source, Vector2 position, int frames, float delay, bool repeat = true) : base(texture, source, position)
        {
            startSource = source;
            this.frames = frames;
            this.delay = delay;
            this.repeat = repeat;
        }
        public SimpleAnimatedSprite(Texture2D texture, Rectangle source, ISticky sticky, int frames, float delay, bool repeat = true) : base(texture, source, sticky)
        {
            startSource = source;
            this.frames = frames;
            this.delay = delay;
            this.repeat = repeat;
        }

        public IAnimatedSprite Play()
        {
            playing = true;
            OnPlay?.Invoke(this, EventArgs.Empty);
            return this;
        }
        public IAnimatedSprite Stop()
        {
            playing = false;
            age = 0;
            OnStop?.Invoke(this, EventArgs.Empty);
            return this;
        }
        public IAnimatedSprite Pause()
        {
            playing = false;
            OnPause?.Invoke(this, EventArgs.Empty);
            return this;
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if (playing)
            {
                age += gametime.ElapsedGameTime.TotalSeconds;
                if (NextFrame >= frames)
                {
                    ReachedEnd?.Invoke(this, EventArgs.Empty);
                    if (repeat)
                        age = 0; // This is to keep age at a reasonable number. Otherwise it would grow without bounds.
                    else
                        playing = false;
                }
            }

            source = new Rectangle(startSource.X + startSource.Width * NextValidFrame, startSource.Y, startSource.Width, startSource.Height);

            base.Draw(gametime, spriteBatch);
        }
    }
}
