using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Sprites
{
    public class HeartSprite : SimpleAnimatedSprite
    {
        private static readonly Rectangle Source = new(0, 48, 16, 16);
        public HeartSprite(Texture2D texture, Vector2 position) : base(texture, Source, 4)
        {
            Offset = position;
            Layer = LayerConstants.Item;
            Delay = 0.2f;
        }
    }
}
