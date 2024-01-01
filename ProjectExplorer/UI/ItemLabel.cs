using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.Items;
using ProjectExplorer.SpriteUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.UI
{
    public class ItemLabel : BaseSprite
    {
        private IItem item = null;
        public IItem Item 
        {
            get 
            {
                return item;
            }
            set
            {
                item = value;
                if (value != null)
                {
                    SpriteDefinition spriteDefinition = item.GetSprite();
                    texture = spriteDefinition.Texture;
                    source = spriteDefinition.Source;
                }
            }
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if (item != null)
            {
                base.Draw(gametime, spriteBatch);
            }
        }
    }
}
