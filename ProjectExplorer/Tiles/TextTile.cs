using ProjectExplorer.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.SpriteUtil.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles
{
    public class TextTile : IGameObject, IMitotic
    {
        private TextSprite textSprite;

        public string Text
        {
            get { return textSprite.Text; }
            set { textSprite.Text = value; }
        }

        public Vector2 Position { get; set; }

        public TextTile(string font, Vector2 position, string text = "") 
        {
            textSprite = new TextSprite(font, text)
            {
                AttachedObject = this,
            };
            Position = position;
        }
        public TextTile(SpriteFont font, Vector2 position, string text = "")
        {
            textSprite = new TextSprite(font, text)
            {
                AttachedObject = this,
            };
            Position = position;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            textSprite.Draw(gameTime, spriteBatch);
        }

        public void SetText(string text)
        {
            textSprite.Text = text;
        }

        public void OnRegister(ILevel level)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            return new TextTile(textSprite.Font, objectDefinition.Position, objectDefinition.GetValue("Text", textSprite.Text));
        }
    }
}
