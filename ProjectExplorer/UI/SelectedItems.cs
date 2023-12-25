using ProjectExplorer.Items.Storage;
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
    public class SelectedItems : ISprite
    {
        private static readonly Vector2 PrimaryOffset = new(24, 0);

        private Vector2 position;
        private IItemSelector selector;
        public SelectedItems(Vector2 position, IItemSelector selector) 
        {
            this.position = position;
            this.selector = selector;
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            selector.Secondary?.GetSprite(position).Draw(gametime, spriteBatch);
            selector.Primary?.GetSprite(position + PrimaryOffset).Draw(gametime, spriteBatch);
        }
    }
}
