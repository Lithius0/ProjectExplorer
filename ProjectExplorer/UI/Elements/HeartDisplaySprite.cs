using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.UI.Elements
{
    public class HeartDisplaySprite : BaseSprite
    {

        public HeartDisplaySprite(Vector2 position, float fullness) : base(new SpriteDefinition("Objects", 128, 2, 14, 14))
        {
            Layer = LayerConstants.Foreground;
            Offset = position;

            if (fullness >= 1)
                source = new Rectangle(64, 2, 14, 14);
            else if (fullness >= 0.75)
                source = new Rectangle(80, 2, 14, 14);
            else if (fullness >= 0.5)
                source = new Rectangle(96, 2, 14, 14);
            else if (fullness >= 0.25)
                source = new Rectangle(112, 2, 14, 14);
            else
                source = new Rectangle(128, 2, 14, 14);
        }
    }
}
