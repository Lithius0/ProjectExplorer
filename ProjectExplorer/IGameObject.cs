using ProjectExplorer.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer
{
    /// <summary>
    /// Anything which needs an update/draw call (except sprites) should implement this.
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// This method will be called whenever the GameObject is registered to a level.
        /// Implementation should save the arguments for deregistration later if it's a temporary object.
        /// If permanent, implementation is optional.
        /// </summary>
        /// <param name="level">The level that the object was registered with</param>
        public void OnRegister(ILevel level);

        public void Update(GameTime gameTime);
        /// <summary>
        /// Draws the object.
        /// Most of the time this should just trigger an ISprite draw call.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
