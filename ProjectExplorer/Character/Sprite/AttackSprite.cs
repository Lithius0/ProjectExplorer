using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Sprite
{
    public class AttackSprite : PlayerSubsprite
    {
        public AttackSprite(Texture2D texture, Rectangle source, ISticky sticky, IPlayer player, int frames = 3) : base(texture, source, sticky, player, frames, PlayerConfig.AttackDuration / frames, false)
        {
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            // AttackDuration can change through the settings.
            delay = PlayerConfig.AttackDuration / frames;
            base.Draw(gametime, spriteBatch);
        }
    }
}
