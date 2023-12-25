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
    /// </summary>
    public class ForegroundTile : IGameObject, ICollidable, IMitotic
    {
        protected ISprite sprite;
        protected Rectangle transform;
        protected ILevel level;
        protected ICollisionHandler collisionHandler;

        protected string texture;
        protected Rectangle source;

        public CollisionGroup Group => CollisionGroup.Tiles;

        public ForegroundTile(Rectangle rectangle, ISprite sprite)
        {
            this.sprite = sprite;
            transform = rectangle;
            collisionHandler = new TileCollisionHandler();
        }
        public ForegroundTile(Vector2 position, string texture, Rectangle source)
        {
            sprite = new BaseSprite(SpriteManager.GetTexture(texture), source, position)
            {
                AnchorPoint = AnchorPoints.TopLeft,
                Layer = LayerConstants.Foreground
            };
            transform = new Rectangle(position.ToPoint(), source.Size);
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
            return transform;
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
