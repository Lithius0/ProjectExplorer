using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    /// <summary>
    /// Consolidate all the sprite loading in one place.
    /// May be used for dynamic loading/unloading if the game gets big.
    /// TODO: Automatic loading
    /// </summary>
    public static class SpriteManager
    {
        private static IDictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private static IDictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void LoadSprites(GraphicsDevice graphics, ContentManager content)
        {
            sprites.Add("Character", content.Load<Texture2D>("Sprites/Player/Character"));

            sprites.Add("Cave", content.Load<Texture2D>("Sprites/World/Cave"));
            sprites.Add("Inner", content.Load<Texture2D>("Sprites/World/Inner"));
            sprites.Add("Objects", content.Load<Texture2D>("Sprites/World/Objects"));
            sprites.Add("Overworld", content.Load<Texture2D>("Sprites/World/Overworld"));
            sprites.Add("ItemsLarge", content.Load<Texture2D>("Sprites/ItemsLarge"));
            sprites.Add("Items", content.Load<Texture2D>("Sprites/Items/Items"));
            sprites.Add("Projectiles", content.Load<Texture2D>("Sprites/Projectiles"));
            sprites.Add("Hud", content.Load<Texture2D>("Sprites/UserInterface/Hud"));
            sprites.Add("Missing", content.Load<Texture2D>("Sprites/Missing"));


            Texture2D pixel = new(graphics, 1, 1);
            pixel.SetData(new[] { Color.White });
            sprites.Add("Pixel", pixel);

            fonts.Add("DuskB2", content.Load<SpriteFont>("DuskB2"));
            fonts.Add("DuskB3", content.Load<SpriteFont>("Sprites/DuskB3"));
        }

        public static Texture2D GetTexture(string name)
        {
            if (sprites.ContainsKey(name))
                return sprites[name];
            else
                throw new KeyNotFoundException($"{name} is not a valid sprite!");
        }

        public static SpriteFont GetFont(string name)
        {
            if (fonts.ContainsKey(name))
                return fonts[name];
            else
                throw new KeyNotFoundException($"{name} is not a valid font!");
        }
    }
}
