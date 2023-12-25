using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
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
    /// Basic tile class
    /// </summary>
    public class DoorTile : IGameObject, ICollidable, IDoor
    {
        protected ISprite overhang;
        protected ISprite spriteOpen;
        protected ISprite spriteClosed;
        protected bool openStatus;
        protected Rectangle transform;

        public bool OpensWithAnyKey { get; set; }

        public DoorTile(Vector2 position, ISprite spriteOpen, ISprite spriteClosed, bool openStatus, bool opensWithAnyKey = true)
        {
            // TODO: Implement overhang
            this.spriteOpen = spriteOpen;
            this.spriteClosed = spriteClosed;
            this.openStatus = openStatus;
            OpensWithAnyKey = opensWithAnyKey;
            transform = new Rectangle(position.ToPoint(), Tiling.ToPixels(2, 2));
        }

        public CollisionGroup Group => CollisionGroup.Tiles;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (openStatus == true)
            {
                spriteOpen.Draw(gameTime, spriteBatch);
            } else
            {
                spriteClosed.Draw(gameTime, spriteBatch);
            }
            //overhang.Draw(gameTime, spriteBatch);
        }

        public Rectangle GetCollider()
        {
            return transform;
        }

        public ICollisionHandler GetCollisionHandler()
        {
            if (openStatus == true)
            {
                return EmptyCollisionHandler.Instance;
            }
            else
            {
                return new TileCollisionHandler();
            }
        }

        public bool IsOpen()
        {
            return openStatus;
        }

        public void OnRegister(ILevel level)
        {
            // Empty
        }

        public void Toggle()
        {
            openStatus = !openStatus;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Intentionally empty. Most tiles don't do much but sit around. 
        }
    }
}
