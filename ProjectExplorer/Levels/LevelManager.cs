using ProjectExplorer.CharacterNS;
using ProjectExplorer.Utility;
using ProjectExplorer.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProjectExplorer.Items;
using ProjectExplorer.Items.ItemEntities;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Manages all the loaded levels in the game as well as handles level transitions.
    /// </summary>
    public class LevelManager : Screen
    {
        private IDictionary<string, ILevel> levels;
        private ILevel activeLevel;
        private IPlayer player; // Player is treated differently since it persists between level changes
        private LevelTransitioner transitioner;
        public ILevel ActiveLevel
        { get { return activeLevel; } }
        public IPlayer Player
        { get { return player; } }
        public float TransitionTime { get; set; } = 1;
        public bool InTransition => transitioner.InTransition;

        public LevelManager(Vector2 position, int layer = 0) : base(position, layer)
        {
            levels = LevelLoader.LoadAll(this);
            player = new Player(new Vector2(100, 100));
            activeLevel = levels["start"];
            activeLevel.Register(player);
            transitioner = new LevelTransitioner(this);
            transitioner.TransitionComplete += OnTransitionComplete;
        }

        public void RegisterLevel(ILevel level)
        {
            levels[level.LevelId] = level;
        }

        public void Update(GameTime gameTime)
        {
            if (InTransition)
                transitioner.Update(gameTime);
            else
                ActiveLevel.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix transformation)
        {
            if (InTransition)
            {
                transitioner.Draw(gameTime, spriteBatch, Transform * transformation);
            }
            else
            {
                ActiveLevel.Draw(gameTime, spriteBatch, Transform * transformation);
            }
            base.Draw(gameTime, spriteBatch, transformation);
        }

        private void ChangeLevel(ILevel level)
        {
            activeLevel.Unload();
            activeLevel.Deregister(player);
            activeLevel = level;
            activeLevel.Register(player);
            activeLevel.Load();
        }


        /// <summary>
        /// Changes the current level.
        /// This one is instantaneous
        /// </summary>
        /// <param name="name">Name of level to change to</param>
        public void ChangeLevel(string name)
        {
            if (InTransition)
                return;

            if (levels.ContainsKey(name))
            {
                ChangeLevel(levels[name]);
            }
            else
            {
                Debug.WriteLine(name + " is not a valid level!");
            }
        }

        /// <summary>
        /// Changes the current level.
        /// This will go through a transition trigger in the direction given.
        /// </summary>
        /// <param name="name">Name of the level to change to.</param>
        /// <param name="direction">Direction of the next level relative to current.</param>
        /// <param name="destination">Place to put the player once transition is complete.</param>
        /// <param name="transitionTime">Time in seconds for the level transition. Uses LevelManager's value is not provided.</param>
        public void ChangeLevel(string name, Direction direction, Vector2? destination, float? transitionTime)
        {
            if (InTransition)
                return;

            if (levels.ContainsKey(name))
            {
                transitioner.Transition(levels[name], direction, transitionTime.GetValueOrDefault(TransitionTime), destination);
            }
            else
            {
                Debug.WriteLine(name + " is not a valid level!");
            }
        }

        /// <summary>
        /// Resets the level manager to its initial state.
        /// <b>Do not call mid-update</b>
        /// </summary>
        public void Reset()
        {
            foreach (ILevel level in levels.Values)
            {
                level.DeregisterAll();
            }
            levels.Clear();

            transitioner.Reset();
            levels = LevelLoader.LoadAll(this);
            player.Reset();
            player.Position = new Vector2(100, 100);
            activeLevel = levels["start"];
            activeLevel.Register(player);
        }

        public IList<ILevel> GetLevelList()
        {
            return levels.Values.ToList();
        }

        public ILevel GetLevel(string id)
        {
            return levels[id];
        }

        private void OnTransitionComplete(object sender, TransitionEventArgs e)
        {
            ChangeLevel(e.NextLevel);
        }
    }
}
