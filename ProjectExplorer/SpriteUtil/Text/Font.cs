using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil.Text
{
    public class Font : IFont
    {
        private IDictionary<char, Point> charMappings;
        private Point size;
        private string textureName;

        public Font(string textureName, IDictionary<char, Point> charMappings, Point size)
        {
            this.textureName = textureName;
            this.charMappings = charMappings;
            this.size = size;
        }

        public Texture2D Texture => SpriteManager.GetTexture(textureName);

        public Rectangle GetSource(char c)
        {
            return new Rectangle(charMappings[c], size);
        }
    }
}
