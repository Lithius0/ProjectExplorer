using ProjectExplorer.DebugMode;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ProjectExplorer.DebugMode
{
    public abstract class InfoModule : ISprite
    {
        private Texture2D backgroundTexture;
        protected SpriteFont font;
        protected float fontScale = 1;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public string Name { get; set; } = "DEBUG";
        public Color BackgroundColor { get; set; } = Color.Black * 0.3f;
        public int Padding { get; set; } = 2; // Pixels

        protected readonly IDictionary<string, string> sections = new Dictionary<string, string>();
        public IDictionary<string, string> Sections => sections;

        protected InfoModule()
        {
            font = SpriteManager.GetFont("DuskB3");
            backgroundTexture = SpriteManager.GetTexture("Pixel");
        }

        protected abstract void UpdateInfo();

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            UpdateInfo();

            // Total size of the whole module, used to draw the background.
            Vector2 totalSize = Vector2.Zero;
            // Location of where the next string will be drawn
            Vector2 position = Position + new Vector2(Padding);

            spriteBatch.DrawString(font, Name, position, Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 1);
            Vector2 size = font.MeasureString(Name) * fontScale;
            totalSize = new Vector2(Math.Max(size.X, totalSize.X), totalSize.Y + size.Y);
            position += new Vector2(0, size.Y);

            foreach (string sectionKey in sections.Keys)
            {
                string section = sectionKey + " " + sections[sectionKey];
                spriteBatch.DrawString(font, section, position, Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 1);

                size = font.MeasureString(section) * fontScale;
                totalSize = new Vector2(Math.Max(size.X, totalSize.X), totalSize.Y + size.Y);
                position += new Vector2(0, size.Y);
            }

            totalSize += new Vector2(2 * Padding);
            spriteBatch.Draw(backgroundTexture, Position, null, BackgroundColor, 0, Vector2.Zero, totalSize, SpriteEffects.None, .9f);
        }
    }
}
