using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private static GraphicsDevice graphics;
        private static IDictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        private static IDictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static SpriteDefinition MissingTexture => new(sprites["Missing"], new Rectangle(0, 0, 16, 16));

        public static void LoadSprites(GraphicsDevice graphics, ContentManager content)
        {
            SpriteManager.graphics = graphics;

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
            sprites.Add("Slime", content.Load<Texture2D>("Sprites/Enemies/Slime"));

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

        public static SpriteDefinition GetSpriteDefinition(string name, Rectangle source)
        {
            if (sprites.ContainsKey(name))
            {
                Texture2D texture = sprites[name];
                if (texture.Bounds.Contains(source))
                {
                    return new SpriteDefinition(texture, source);
                }
                else
                {
                    return MissingTexture;
                }
            }
            else
            {
                return MissingTexture;
            }
        }

        public static SpriteFont GetFont(string name)
        {
            if (fonts.ContainsKey(name))
                return fonts[name];
            else
                throw new KeyNotFoundException($"{name} is not a valid font!");
        }

        public static Texture2D GetMissingTexture(Point size)
        {
            Texture2D texture = new(graphics, size.X, size.Y);
            Color[] data = new Color[size.X * size.Y];
            for(int row = 0; row < size.Y; row++)
            {
                for(int col = 0; col < size.X; col++)
                {
                    Color color = Color.Black;
                    // Top left or bottom right are purple
                    if (row < size.Y / 2 && col < size.X / 2 || row > size.Y / 2 && col > size.X / 2) 
                    {
                        color = Color.Purple;
                    }
                    data[row * size.X + col] = color;
                }
            }
            texture.SetData(new[] { Color.White });
            return texture;
        }
    }
}
