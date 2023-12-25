using ProjectExplorer.Items;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.CharacterNS.Sprite
{
    /// <summary>
    /// The reason this is a class of its own rather than being just a method in a factory
    /// is because the item sprite needs to be synced.
    /// This is an animated sprite because of the use of the ReachedEnd event 
    /// used for controlling player animations.
    /// </summary>
    public class PickupSprite : SimpleAnimatedSprite
    {
        private static Rectangle Source = new(144, 0, 16, 32);
        private ISprite itemSprite;

        public PickupSprite(Texture2D texture, Vector2 position, IItem item, float delay, bool twoHanded) : base(texture, Source, position, 1, delay, false)
        {
            if (twoHanded)
            {
                startSource = new Rectangle(208, 0, 32, 32);
                itemSprite = item.GetSprite(position + new Vector2(8, -8));
            }
            else
            {
                itemSprite = item.GetSprite(position + new Vector2(0, -8));
            }

            Layer = LayerConstants.Player;
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            itemSprite.Draw(gametime, spriteBatch);
            base.Draw(gametime, spriteBatch);
        }
    }
}
