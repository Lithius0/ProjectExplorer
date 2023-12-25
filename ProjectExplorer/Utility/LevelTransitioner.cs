using ProjectExplorer.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Utility
{
    /// <summary>
    /// Class responsible for handling level transitions
    /// TODO: Make this class inherit or use the Transitioner class. Or replace this class entirely.
    /// </summary>
    public class LevelTransitioner
    {
        private LevelManager manager;
        private ILevel next = null;

        private float timer = 0;
        private float duration = 1; // Default value. This is updated every transition.

        private Direction direction;
        private Vector2? destination;

        /// <summary>
        /// 1 indicates transition complete. 0 indicates transition just started
        /// </summary>
        public float Progress => Math.Clamp(timer / duration, 0, 1);
        public bool InTransition => next != null && timer < duration;

        public event EventHandler<TransitionEventArgs> TransitionComplete;


        public LevelTransitioner(LevelManager manager)
        {
            this.manager = manager;
        }

        public void Transition(ILevel next, Direction direction, float duration, Vector2? destination = null)
        {
            timer = 0;
            this.duration = duration;
            this.next = next;
            this.direction = direction;
            this.destination = destination;
        }

        public void Update(GameTime gameTime)
        {
            if (InTransition)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= duration)
                {
                    PlacePlayer();
                }
            }
        }

        private void PlacePlayer()
        {
            if (destination.HasValue)
            {
                manager.Player.Position = destination.Value;
            }
            else
            {
                Vector2 position = manager.Player.Position;
                Vector2 size = manager.Player.GetTransform().Size.ToVector2();
                switch (direction)
                {
                    case Direction.UP:
                        position = new Vector2(position.X, Tiling.LevelSize.Y - size.Y - 1);
                        break;
                    case Direction.DOWN:
                        position = new Vector2(position.X, 1);
                        break;
                    case Direction.LEFT:
                        position = new Vector2(Tiling.LevelSize.X - size.X - 1, position.Y);
                        break;
                    case Direction.RIGHT:
                        position = new Vector2(1, position.Y);
                        break;
                }
                manager.Player.Position = position;
            }
            TransitionComplete?.Invoke(this, new TransitionEventArgs(next));
        }

        public Matrix PreviousMatrix()
        {
            Vector2 offset = Tiling.LevelSize.ToVector2() * -direction.GetVector2() * Progress;
            return Matrix.CreateTranslation(new Vector3(offset.X, offset.Y, 0));
        }

        /// <summary>
        /// Gets a matrix which is translated by the given direction by the given multiplier.
        /// With a multiplier of 1, the matrix is translated one full screen away.
        /// </summary>
        private static Matrix GetOffset(Direction direction, float multiplier)
        {
            Vector2 offset = Tiling.LevelSize.ToVector2() * direction.GetVector2() * multiplier;

            return Matrix.CreateTranslation(offset.X, offset.Y, 0);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix matrix)
        {
            // Current screen is moved in the opposite direction.
            Matrix previousOffset = GetOffset(direction.Flip(), Progress);
            // Multiplication order is intentional, we want this translation applied first.
            manager.ActiveLevel.Draw(gameTime, spriteBatch, previousOffset * matrix);

            Matrix nextOffset = GetOffset(direction, 1 - Progress);
            next.Draw(gameTime, spriteBatch, nextOffset * matrix);
        }

        public void Reset()
        {
            timer = 0;
            next = null;
        }
    }

    public class TransitionEventArgs : EventArgs
    {
        public ILevel NextLevel { get; }

        public TransitionEventArgs(ILevel nextLevel)
        {
            NextLevel = nextLevel;
        }
    }
}