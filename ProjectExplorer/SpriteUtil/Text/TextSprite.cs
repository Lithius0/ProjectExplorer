using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectExplorer.SpriteUtil.Text
{
    public class TextSprite : ISprite
    {
        private SpriteFont font;
        public IGameObject AttachedObject { get; set; } = null;
        public SpriteFont Font => font;
        public float Scale { get; set; } = 1;
        public float Layer { get; set; } = 1;
        public Vector2 Offset { get; set; }
        public string Text { get; set; }

        public TextSprite(string font, string text = "")
        {
            this.font = SpriteManager.GetFont(font);
            Layer = LayerConstants.Text;
            Text = text;
        }
        public TextSprite(SpriteFont font, string text = "")
        {
            this.font = font;
            Layer = LayerConstants.Text;
            Text = text;
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Vector2 position = Offset;
            if (AttachedObject != null)
            {
                position += AttachedObject.Position;
            }

            spriteBatch.DrawString(font, Text, position, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, Layer);
        }
    }
}
