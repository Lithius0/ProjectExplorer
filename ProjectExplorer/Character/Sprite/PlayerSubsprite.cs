using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Sprite
{
    /// <summary>
    /// Used for handling one individual animation that Player goes through.
    /// </summary>
    public class PlayerSubsprite : SimpleAnimatedSprite
    {
        private IPlayer player;

        public PlayerSubsprite(Texture2D texture, Rectangle source, IPlayer player, int frames, float delay, bool repeat) : base(texture, source, player, frames, delay, repeat)
        {
            this.player = player;
            Layer = LayerConstants.Player;
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Color = player.RecentlyDamaged ? Color.Red : Color.White;

            base.Draw(gametime, spriteBatch);
        }
    }
}
