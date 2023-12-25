using ProjectExplorer.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Extension of ObjectManager with additional responsibilities.
    /// Carries a LevelId and has (will have) load/unload responsibilities.
    /// This should also be the layer where matrix transformations happen.
    /// </summary>
    public interface ILevel : IObjectManager
    {
        public string LevelId { get; }
        public Point MapPosition { get; } // Position of this level on the map
        /// <summary>
        /// Returns the last valid spot (i.e. no collision) between the source and the end rectangle.
        /// </summary>
        Rectangle LastValidSpotBetween(ICollidable source, Rectangle end);
        public LevelManager Manager { get; }
        /// <summary>
        /// Draw all included objects with the included matrix.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix matrix);

        /// <summary>
        /// Called when level becomes the active level.
        /// </summary>
        public void Load();

        /// <summary>
        /// Called when the level no longer is active.
        /// </summary>
        public void Unload();

        /// <summary>
        /// Tags an object with the given string.
        /// Allows for easier search later.
        /// </summary>
        public void Tag(IGameObject obj, string tag);
        /// <summary>
        /// Gets all objects in level with the given tag.
        /// </summary>
        public IGameObject[] GetObjectsWithTag(string tag);

        public event EventHandler OnLoad;
        public event EventHandler OnUnload;
    }
}
