using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    public interface IAnimatedSprite : ISprite
    {
        public event EventHandler OnPlay;
        public event EventHandler OnStop;
        public event EventHandler OnPause;

        /// <summary>
        /// Will fire once an animation reached the end. Will still fire even if animation repeats.
        /// </summary>
        public event EventHandler ReachedEnd;

        /// <summary>
        /// Play the animation.
        /// If animation was previously paused, this will play from the previously paused position.
        /// </summary>
        /// <returns>
        /// Itself, to allow for play on construction like so:
        /// <code>ISprite s = (new AnimatedSprite()).Play()</code>
        /// </returns>
        public IAnimatedSprite Play();

        /// <summary>
        /// Stops the animation and returns to the starting frame.
        /// </summary>
        /// <returns>
        /// Itself, primarily to keep consistency with <see cref="Play"/>;
        /// </returns>
        public IAnimatedSprite Stop();

        /// <summary>
        /// Stops playing the animation but does not return to the starting frame.
        /// </summary>
        /// <returns>
        /// Itself, primarily to keep consistency with <see cref="Play"/>;
        /// </returns>
        public IAnimatedSprite Pause();
    }
}
