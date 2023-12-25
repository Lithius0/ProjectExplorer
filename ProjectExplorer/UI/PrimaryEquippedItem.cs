using ProjectExplorer.Items;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.UI
{
    public class PrimaryEquippedItem : ISprite
    {
        private static Rectangle Source = new(0, 0, 48, 48);

        private Vector2 position;
        private ISprite borderSprite;

        public PrimaryEquippedItem(Vector2 position)
        {
            this.position = position;
            borderSprite = new BaseSprite(SpriteManager.GetTexture("Hud"), Source, position)
            {
                AnchorPoint = AnchorPoints.TopLeft,
                Color = Color.Red,
            };
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            IPlayer player = Coordinator.Instance.Player;
            IItem primary = player.ItemSelector.Primary;

            if (primary != null)
            {
                ISprite sprite = primary.GetSprite(position + Source.Size.ToVector2() / 2);
                sprite.Draw(gametime, spriteBatch);
            }
            borderSprite.Draw(gametime, spriteBatch);
        }
    }
}
