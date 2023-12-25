using ProjectExplorer.Character;
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
    public class EquippedItems : ISprite
    {
        private static readonly Vector2 PrimaryLocation = new(156, 32);
        private static readonly Vector2 SecondaryLocation = new(132, 32);

        private IPlayer player;

        public EquippedItems(IPlayer player) 
        {
            this.player = player;
        }
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            player.ItemSelector.Primary?.GetSprite(PrimaryLocation).Draw(gametime, spriteBatch);
            player.ItemSelector.Secondary?.GetSprite(SecondaryLocation).Draw(gametime, spriteBatch);
        }
    }
}
