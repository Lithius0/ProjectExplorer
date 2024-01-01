using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    public readonly struct SpriteDefinition
    {
        public readonly Texture2D Texture;
        public readonly Rectangle Source;

        public SpriteDefinition(Texture2D texture, Rectangle source)
        {
            Texture = texture;
            Source = source;
        }
    }
}
