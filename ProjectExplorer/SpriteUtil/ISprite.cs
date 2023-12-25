using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    public interface ISprite
    {
        /// <summary>
        /// Draws the sprite.
        /// This method does not call spriteBatch.Begin() or spriteBatch.End().
        /// It is only responsible for the draw call.
        /// </summary>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch);
    }
}
