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

namespace ProjectExplorer.Character.Sprite
{
    /// <summary>
    /// The reason this is a class of its own rather than being just a method in a factory
    /// is because the item sprite needs to be synced.
    /// This is an animated sprite because of the use of the ReachedEnd event 
    /// used for controlling player animations.
    /// </summary>
    public class PickupSprite : SimpleAnimatedSprite
    {
        private static Rectangle Source = new(208, 0, 32, 32);
        private ISprite itemSprite;

        public PickupSprite(IPlayer player, IItem item) : base(new SpriteDefinition("Character", Source), 1)
        {
            Repeat = false;
            AttachedObject = player;
            Layer = LayerConstants.Player;
            itemSprite = new BaseSprite(item.GetSprite())
            {
                AttachedObject = player,
                Layer = LayerConstants.Player + 0.01f,
                Offset = new Vector2(0, -8),
            };
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            itemSprite.Draw(gametime, spriteBatch);
            base.Draw(gametime, spriteBatch);
        }
    }
}
