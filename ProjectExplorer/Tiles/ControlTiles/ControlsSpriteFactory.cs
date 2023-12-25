using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    public class ControlsSpriteFactory
    {
        private static ControlsSpriteFactory instance = new();
        public static ControlsSpriteFactory Instance => instance;

        private Texture2D spriteSheet;

        private ControlsSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            spriteSheet = content.Load<Texture2D>("Sprites/form_controls");
        }

        public ISprite GetButtonSprite(Vector2 position, Button button)
        {
            return new ButtonSprite(spriteSheet, position, button);
        }

        public ISprite GetSliderSprite(Vector2 position)
        {
            return new BaseSprite(spriteSheet, new Rectangle(0, 16, 48, 16), position)
            {
                Layer = LayerConstants.Background
            };
        }

        public ISprite GetSliderThumbSprite(ISticky sticky)
        {
            return new BaseSprite(spriteSheet, new Rectangle(48, 16, 8, 16), sticky)
            {
                AnchorPoint = AnchorPoints.TopMiddle,
                Layer = LayerConstants.Foreground + 0.01f
            };
        }
    }
}
