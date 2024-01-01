using ProjectExplorer.Items;
using ProjectExplorer.Character;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Utility;

namespace ProjectExplorer.UI
{
    public class PrimaryEquippedItem : ISprite
    {
        private static Rectangle Source = new(0, 0, 48, 48);

        private ISprite borderSprite;
        private ItemLabel primaryItem;

        public PrimaryEquippedItem(Vector2 position)
        {
            borderSprite = new BaseSprite(new SpriteDefinition("Hud", Source))
            {
                Offset = position,
                AnchorPoint = AnchorPoints.TopLeft,
                Color = Color.Red,
            };
            primaryItem = new ItemLabel()
            {
                Offset = position + Source.Size.ToVector2() / 2,
                AnchorPoint = AnchorPoints.Middle,
                Layer = 1,
            };
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            IPlayer player = Coordinator.Instance.Player;
            IItem primary = player.ItemSelector.Primary;

            if (primary != null)
            {
                primaryItem.Item = primary;
                primaryItem.Draw(gametime, spriteBatch);
            }
            borderSprite.Draw(gametime, spriteBatch);
        }
    }
}
