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
using System.Transactions;

namespace ProjectExplorer.Tiles
{
    /// <summary>
    /// Foreground tile class
    /// TODO: Allow variable sizes
    /// </summary>
    public class ForegroundTile : IGameObject, ICollidable, IMitotic
    {
        protected ISprite sprite;
        protected Point size;
        protected ILevel level;
        protected ICollisionHandler collisionHandler;

        protected string texture;
        protected Rectangle source;

        public Vector2 Position { get; set; }
        public CollisionGroup Group => CollisionGroup.Tiles;

        public ForegroundTile(Vector2 position, ISprite sprite)
        {
            this.sprite = sprite;
            Position = position;
            collisionHandler = new TileCollisionHandler();
        }
        public ForegroundTile(Vector2 position, string texture, Rectangle source)
        {
            sprite = new BaseSprite(new SpriteDefinition(texture, source))
            {
                AnchorPoint = AnchorPoints.TopLeft,
                Layer = LayerConstants.Foreground,
                AttachedObject = this,
            };
            Position = position;
            collisionHandler = new TileCollisionHandler();
            this.texture = texture;
            this.source = source;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }

        public Rectangle GetCollider()
        {
            return new Rectangle(Position.ToPoint(), Tiling.Full);
        }

        public virtual ICollisionHandler GetCollisionHandler()
        {
            return collisionHandler;
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Intentionally empty. Most tiles don't do much but sit around. 
        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            return new ForegroundTile(objectDefinition.Position, texture, source);
        }
    }
}
