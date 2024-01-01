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
    /// 
    /// tile class
    /// </summary>
    public class BackgroundTile : IGameObject, IMitotic
    {
        protected ISprite sprite;

        // Purely here for cloning.
        protected string texture;
        protected Rectangle source;
        
        public Vector2 Position { get; set; }

        public BackgroundTile(ISprite sprite)
        {
            this.sprite = sprite;
        }
        public BackgroundTile(Vector2 position, string texture, Rectangle source)
        {
            sprite = new BaseSprite(SpriteManager.GetTexture(texture), source)
            {
                AnchorPoint = AnchorPoints.TopLeft,
                Layer = LayerConstants.Background,
                AttachedObject = this,
            };
            Position = position;
            this.texture = texture;
            this.source = source;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }

        public void OnRegister(ILevel level)
        {
            //Empty
        }

        public virtual void Update(GameTime gameTime)
        {
            // Intentionally empty. Most tiles don't do much but sit around. 
        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            return new BackgroundTile(objectDefinition.Position, texture, source);
        }
    }
}
