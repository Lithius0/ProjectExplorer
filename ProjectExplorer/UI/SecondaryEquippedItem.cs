using ProjectExplorer.Items;
using ProjectExplorer.Character;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.SpriteUtil.Text;
using ProjectExplorer.Utility;

namespace ProjectExplorer.UI
{
    public class SecondaryEquippedItem : ISprite
    {
        private static Rectangle Source = new(0, 0, 48, 48);

        private TextSprite amountDisplay;
        private ISprite borderSprite;
        private ItemLabel secondaryItem;

        public SecondaryEquippedItem(Vector2 position)
        {
            amountDisplay = new TextSprite("DuskB3", "0")
            { 
                Offset = position + new Vector2(32, 32) 
            };
            borderSprite = new BaseSprite(new SpriteDefinition("Hud", Source))
            {
                Offset = position,
                AnchorPoint = AnchorPoints.TopLeft,
                Color = Color.Blue,
            };
            secondaryItem = new ItemLabel()
            {
                Offset = position + Source.Size.ToVector2() / 2,
                AnchorPoint = AnchorPoints.Middle,
                Layer = 1,
            };
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            IPlayer player = Coordinator.Instance.Player;
            IItem secondary = player.ItemSelector.Secondary;

            if (secondary != null)
            {
                int amount = player.Inventory.AmountOf(secondary);
                secondaryItem.Item = secondary;
                secondaryItem.Draw(gametime, spriteBatch);
                amountDisplay.Text = amount.ToString();
                amountDisplay.Draw(gametime, spriteBatch);
            }
            borderSprite.Draw(gametime, spriteBatch);
        }
    }
}
