using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    public readonly struct SpriteDefinition
    {
        public readonly Texture2D Texture;
        public readonly Rectangle Source;

        /// <summary>
        /// Manual creation of a sprite definition. 
        /// Direct assignment of texture and source without any checks.
        /// </summary>
        public SpriteDefinition(Texture2D texture, Rectangle source)
        {
            Texture = texture;
            Source = source;
        }

        /// <summary>
        /// Gets texture from the SpriteManager class.
        /// Will return the missing sprite definition if the texture or source is invalid.
        /// </summary>
        public SpriteDefinition(string texture, Rectangle source)
        {
            try
            {
                Texture = SpriteManager.GetTexture(texture);
                if (Texture.Bounds.Contains(source))
                {
                    Source = source;
                }
                else
                {
                    Debug.WriteLine($"Invalid source: { source } in [{ texture }]!");
                    Texture = SpriteManager.MissingTexture.Texture;
                    Source = SpriteManager.MissingTexture.Source;
                }
            }
            catch (KeyNotFoundException)
            {
                Debug.WriteLine($"Invalid texture: [{ texture }]!");
                Texture = SpriteManager.MissingTexture.Texture;
                Source = SpriteManager.MissingTexture.Source;
            }
        }

        /// <summary>
        /// Alternative to <see cref="SpriteDefinition(string, Rectangle)"/>.
        /// Purely for convenience.
        /// </summary>
        public SpriteDefinition(string texture, int x, int y, int width, int height) : this(texture, new Rectangle(x, y, width, height))
        { 
        }
    }
}
