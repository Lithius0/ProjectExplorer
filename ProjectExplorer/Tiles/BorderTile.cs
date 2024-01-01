using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles
{
    /// <summary>
    /// Border tile class
    /// </summary>
    public class BorderTile : IGameObject, ICollidable
    {
        protected Rectangle transform;

        public BorderTile(Vector2 position, int width, int height)
        {
            transform = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public CollisionGroup Group => CollisionGroup.Tiles;

        public Vector2 Position { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Empty
        }

        public Rectangle GetCollider()
        {
            return transform;
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return new TileCollisionHandler();
        }

        public void OnRegister(ILevel level)
        {
            // Empty
        }

        public virtual void Update(GameTime gameTime)
        {
            // Intentionally empty. Most tiles don't do much but sit around. 
        }
    }
}
