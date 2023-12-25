using ProjectExplorer.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using ProjectExplorer.Tiles;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Levels;
using ProjectExplorer.Projectiles.Sprites;
using ProjectExplorer.Controllers;
using ProjectExplorer.Character.Sprite;
using ProjectExplorer.DebugMode;
using ProjectExplorer.UI;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Tiles.ControlTiles;
using ProjectExplorer.Utility;

namespace ProjectExplorer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IController keyboardController;
        private IController mouseController;
        private LevelManager levelManager;
        private ScreenManager screen;
        public ScreenManager Screen => screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 512;
            _graphics.PreferredBackBufferHeight = 288;
        }

        protected override void Initialize()
        {
            Debug.WriteLine("Initializing");

            base.Initialize();

            levelManager = new LevelManager(Tiling.ToPixels(0, 4).ToVector2());
            screen = new ScreenManager(_graphics);
            screen.Scale = 3;

            Coordinator.Instance.Initialize(screen, levelManager);

            // User input
            KeyboardController keyboardController = new();
            this.keyboardController = keyboardController;
            MouseController mouseController = new();
            this.mouseController = mouseController;

            KeyboardCommands.RegisterPlayerCommands(keyboardController, levelManager.Player);
            KeyboardCommands.RegisterGameCommands(keyboardController, this, screen);
            MouseCommands.RegisterGameCommands(mouseController, this, levelManager);

            Debug.WriteLine("Initialized");

        }

        protected override void LoadContent()
        {
            Debug.WriteLine("Content loading");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteManager.LoadSprites(GraphicsDevice, Content);
            
            ObjectRegistry.Register();

            Debug.WriteLine("Content loaded");
        }

        protected override void Update(GameTime gameTime)
        {
            Coordinator.Instance.Update(gameTime);
            if (IsActive)
            {
                keyboardController.Update();
                mouseController.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            screen.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}