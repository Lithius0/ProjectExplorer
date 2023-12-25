using ProjectExplorer.DebugMode;
using ProjectExplorer.Items;
using ProjectExplorer.Items.Storage;
using ProjectExplorer.Levels;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.CharacterNS.Sprite;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.UI;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectExplorer
{
    public enum GameState
    {
        Inventory,
        World,
        Pause,
        GameOver,
    }

    /// <summary>
    /// A top-level class to coordinate all of the various elements of the game.
    /// Also exposes the instances of ScreenManager and LevelManager currently in use.
    /// This class is a singleton to allow for easy access, but in general reaching up here from below is a code smell, and a bad one.
    /// The management classes, such as ILevel, LevelManager, IScreen and ScreenManager definitely should not reach here.
    /// <br></br>
    /// The point of this class is to be the nucleus of a lot of necessary coupling.
    /// This is to free the level management and UI management classes to be as loose as is possible.
    /// If making a new game from this framework, all that would need to be replaced is many of the 
    /// low-level game objects (which is unavoidable) and this class, the Coordinator.
    /// </summary>

    public class Coordinator
    {
        private static readonly Coordinator instance = new();
        public static Coordinator Instance => instance;

        public ScreenManager ScreenManager { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public GameState State { get; private set; }
        public bool HardMode { get; set; } = false;
        /// <summary>
        /// Gets the currently active player.
        /// Generally speaking, IGameObjects should access the player through the level.
        /// This is here for anything outside the LevelManager environment.
        /// </summary>
        public IPlayer Player => LevelManager.Player;

        // If true, the next frame will be played even if paused. Allows for frame by frame stepping.
        private bool playNext = false;

        private bool resetting = false;

        private IScreen debug;
        private IScreen inventoryHud;

        private Delayer respawnDelay;

        private Coordinator() { }

        /// <summary>
        /// Initialization method.
        /// Should only be called from Game.
        /// </summary>
        public void Initialize(ScreenManager screenManager, LevelManager levelManager)
        {
            ScreenManager = screenManager;
            LevelManager = levelManager;
            LevelManager.Position = new Vector2(0, 56);

            inventoryHud = ScreenFactory.Instance.CreateInventoryHud(Tiling.ToPixels(0, -15).ToVector2(), LevelManager);
            //pause = ScreenFactory.Instance.CreatePauseScreen(new Vector2(0, 56));
            debug = new DebugInfo(levelManager.Player);

            ScreenManager.Screens.Add(levelManager);
            ScreenManager.Screens.Add(new MainHud(new Vector2(0, 0)));
            ScreenManager.Screens.Add(inventoryHud);
            //ScreenManager.Screens.Add(ScreenFactory.Instance.CreateHud(Vector2.Zero, LevelManager));

            State = GameState.World;
            respawnDelay = new Delayer(3, false);
            respawnDelay.End += (_, _) =>
            {
                Reset();
            };

            levelManager.Player.Died += OnDeath;
        }

        /// <summary>
        /// Update method.
        /// Should only be called from Game.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if ((State == GameState.World || playNext) && !ScreenManager.Camera.InMotion)
            {
                LevelManager.Update(gameTime);
                playNext = false;
            }

            // Don't want to reset mid-update.
            if (resetting)
            {
                LevelManager.Reset();
                State = GameState.World;
                resetting = false;
            }

            if (State == GameState.GameOver)
            {
                respawnDelay.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw method.
        /// Should only be called from Game.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            ScreenManager.Draw(gameTime, spriteBatch);
            // Level manager is drawn through screen manager since it is also a screen.
        }

        public void PlayNext()
        {
            playNext = true;
        }

        public void ToggleInventory()
        {
            if (State == GameState.World)
            {
                ScreenManager.Camera.FocusToWithSpeed(inventoryHud, Tiling.ToPixels(16));
                State = GameState.Inventory;
            }
            else if (State == GameState.Inventory)
            {
                ScreenManager.Camera.MoveToWithSpeed(Vector2.Zero, Tiling.ToPixels(16));
                State = GameState.World;
            }
        }

        public void TogglePause()
        {
            if (State == GameState.World) 
            {
                State = GameState.Pause;            
            } else if (State == GameState.Pause)
            {
                State = GameState.World;
            }
        }

        public void ToggleDebug()
        {
            if (ScreenManager.Screens.Contains(debug))
            {
                ScreenManager.Screens.Remove(debug);
            }
            else
            {
                ScreenManager.Screens.Add(debug);
            }
        }

        public void OnDeath(object sender, EventArgs e)
        {
            State = GameState.GameOver;
            respawnDelay.Restart();
        }

        public void Reset()
        {
            resetting = true;
        }
    }
}
