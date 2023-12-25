using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil.Text
{
    public interface IFont
    {
        public Texture2D Texture { get; }

        public Rectangle GetSource(char c);
    }
}
