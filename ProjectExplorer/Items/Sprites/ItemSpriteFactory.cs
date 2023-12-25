using ProjectExplorer.Projectiles;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Sprites
{
    public class ItemSpriteFactory
    {
        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get { return instance; }
        }

        private ItemSpriteFactory()
        {
        }

        public ISprite GetBombSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 0, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }

        public ISprite GetCoinSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Objects"), new Rectangle(0, 64, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }

        public ISprite GetHeartContainerSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Objects"), new Rectangle(64, 0, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }

        public ISprite GetKeySprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 64, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }
        public IAnimatedSprite GetHeartSprite(Vector2 position)
        {
            return new HeartSprite(SpriteManager.GetTexture("Objects"), position);
        }
        public ISprite GetStarterSwordSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 16, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }
        public ISprite GetWindSwordSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 32, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }
        public ISprite GetMoonSwordSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 48, 16, 16), position) 
            { 
                Layer = LayerConstants.Item 
            };
        }
        public ISprite GetMapSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 64, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }
        public ISprite GetBowSprite(Vector2 position)
        {
            return new BaseSprite(SpriteManager.GetTexture("Items"), new Rectangle(0, 96, 16, 16), position)
            {
                Layer = LayerConstants.Item,
            };
        }
    }
}
