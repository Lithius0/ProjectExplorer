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
        public AttackSprite(SpriteDefinition spriteDefinition, IPlayer player, int frames = 3) : base(spriteDefinition, player, frames)
        {
            Repeat = false;
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            // Duration can change during runtime.
            Duration = PlayerConfig.AttackDuration;
            base.Draw(gametime, spriteBatch);
        }
    }
}
