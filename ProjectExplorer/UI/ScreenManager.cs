using ProjectExplorer.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Items;
using ProjectExplorer.CharacterNS;
using Microsoft.Xna.Framework.Input;
using ProjectExplorer.Utility;

namespace ProjectExplorer.UI
{
    public class ScreenManager
    {
        private readonly GraphicsDeviceManager graphics;

        private readonly int defaultWidth;
        private readonly int defaultHeight;

        // No real point to hiding this beyond disallowing reassignment. 
        // The add and remove methods will need to be exposed anyway.
        public ISet<IScreen> Screens { get; private set; }

        private Camera camera;
        public Camera Camera { get { return camera; } }
        public Rectangle GameWindow => new(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

        private int scale = 1;
        private bool wide = false;
        public int Scale
        {
            get => scale;
            set
            {
                scale = value;
                graphics.PreferredBackBufferWidth = defaultWidth * scale;
                graphics.PreferredBackBufferHeight = defaultHeight * scale;
                graphics.ApplyChanges();

            }
        }

        public ScreenManager(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            defaultWidth = graphics.PreferredBackBufferWidth;
            defaultHeight = graphics.PreferredBackBufferHeight;

            Screens = new HashSet<IScreen>();
            camera = new Camera(Vector2.Zero);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Don't scale the Z-axis, the layer depth can get scaled > 1 and make things disappear. 
            Matrix scaleMatrix = Matrix.CreateScale(scale, scale, 1);

            camera.Draw(gameTime, spriteBatch, scaleMatrix, Screens);
        }
    }
}
