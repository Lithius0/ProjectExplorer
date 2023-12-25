using ProjectExplorer.Items;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.SpriteUtil.Text;

namespace ProjectExplorer.UI
{
    public class SecondaryEquippedItem : ISprite
    {
        private static Rectangle Source = new(0, 0, 48, 48);

        private Vector2 position;
        private TextSprite amountDisplay;
        private ISprite borderSprite;

        public SecondaryEquippedItem(Vector2 position)
        {
            this.position = position;
            amountDisplay = new TextSprite("DuskB3", this.position + new Vector2(32, 32), "0");
            borderSprite = new BaseSprite(SpriteManager.GetTexture("Hud"), Source, position)
            { 
                AnchorPoint = AnchorPoints.TopLeft,
                Color = Color.Blue,
            };
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            IPlayer player = Coordinator.Instance.Player;
            IItem secondary = player.ItemSelector.Secondary;

            if (secondary != null)
            {
                int amount = player.Inventory.AmountOf(secondary);
                ISprite sprite = secondary.GetSprite(position + Source.Size.ToVector2() / 2);
                sprite.Draw(gametime, spriteBatch);
                amountDisplay.Text = amount.ToString();
                amountDisplay.Draw(gametime, spriteBatch);
            }
            borderSprite.Draw(gametime, spriteBatch);
        }
    }
}
