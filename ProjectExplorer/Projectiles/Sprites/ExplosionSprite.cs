using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Projectiles.Sprites
{
    public class ExplosionSprite : SimpleAnimatedSprite
    {
        public ExplosionSprite(Vector2 position) : base(SpriteManager.GetTexture("Objects"), new Rectangle(64, 64, 32, 32), position, 7, 1/10f, false)
        {
            Layer = LayerConstants.Player;
        }
    }
}
