using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectExplorer.UI
{
    /// <summary>
    /// Interface responsible for drawing a batch of sprites with the given transformation.
    /// </summary>
    public interface IScreen
    {
        public Vector2 Position { get; set; }
        public int Layer { get; set; }

        public void AddModule(ISprite module);
        public void RemoveModule(ISprite module);

        /// <summary>
        /// Draw the sprites of the screen with the given transformation.
        /// </summary>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch, Matrix transformation);
    }
}