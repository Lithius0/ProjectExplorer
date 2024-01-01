using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Levels;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items
{
    /// <summary>
    /// Class of a bomb after it has been placed.
    /// This doesn't really fit in this folder, but it also doesn't really fit in projectile either.
    /// So here it will stay.
    /// </summary>
    public class PlacedBomb : IGameObject
    {
        private float fuse; // Amount of time remaining until the bomb explodes.
        private ILevel level;
        private ISprite sprite;

        public Vector2 Position { get; set; }

        public bool Exploded
        {
            get { return fuse <= 0; }
        }

        public PlacedBomb(float fuse, Vector2 position)
        {
            this.fuse = fuse;
            Position = position;

            sprite = new BaseSprite(Bomb.Instance.GetSprite())
            {
                Layer = LayerConstants.Item,
                AttachedObject = this,
            };
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            fuse -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (fuse <= 0)
            {
                level.Register(new Explosion(Position));
                SoundFactory.Instance.PlaySound("BombBlow");
                level?.Deregister(this);
            }
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }
    }
}
