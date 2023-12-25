using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectExplorer.SpriteUtil.Text
{
    public class TextSprite : ISprite
    {
        private SpriteFont font;
        public SpriteFont Font => font;
        public float Scale { get; set; } = 1;
        public float Layer { get; set; } = 1;
        public Vector2 Position { get; set; }
        public string Text { get; set; }

        public TextSprite(string font, Vector2 position, string text = "")
        {
            this.font = SpriteManager.GetFont(font);
            Position = position;
            Layer = LayerConstants.Text;
            Text = text;
        }
        public TextSprite(SpriteFont font, Vector2 position, string text = "")
        {
            this.font = font;
            Position = position;
            Layer = LayerConstants.Text;
            Text = text;
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, Position, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, Layer);
        }
    }
}
