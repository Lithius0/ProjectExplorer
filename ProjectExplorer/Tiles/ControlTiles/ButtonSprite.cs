using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    public class ButtonSprite : BaseSprite
    {
        private static Rectangle UpSource = new(0, 0, 16, 16);
        private static Rectangle DownSource = new(16, 0, 16, 16);
        private Button button;

        public ButtonSprite(Texture2D texture, Vector2 position, Button button) : base(texture, UpSource, position)
        {
            Layer = LayerConstants.Foreground + 0.01f;
            this.button = button;
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            source = button.On ? DownSource : UpSource;
            base.Draw(gametime, spriteBatch);
        }
    }
}
