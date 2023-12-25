using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProjectExplorer.UI
{
    /// <summary>
    /// Since MonoGame doesn't have a way to position the viewport, this class is here to provide that functionality.
    /// It's also here to hide some of the matrix math so the other classes don't need to think about it.
    /// 
    /// Conceptually, this class places its screens on a virtual plane and moves the viewing screen to them.
    /// More concretely, this class calculates the matrix to transforms the screens and the "camera" is immobile.
    /// </summary>
    public class Camera
    {
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 target;

        private float timer = 1;
        private float duration = 1;

        public float Progress => duration > 0 ? Math.Clamp(timer / duration, 0, 1) : 1; // Return 1 if duration is invalid
        public bool InMotion => timer < duration;
        public event EventHandler MoveComplete;

        public Matrix GetTransform()
        {
            // Moving the camera by a given displacement is identical to moving everything else with the inverse displacement
            // Since we can't actually change where the screen draws from, we're using the latter method.
            return Matrix.CreateTranslation(-position.X, -position.Y, 0);
        }

        public Camera(Vector2 position) 
        {
            this.position = position;
        }

        /// <summary>
        /// Move the camera to the given location in the specified time.
        /// </summary>
        /// <param name="position">Position to move to</param>
        /// <param name="duration">Seconds it takes for the move to complete.</param>
        public void MoveToInTime(Vector2 position, float duration)
        {
            target = position;
            Vector2 displacement = position - this.position;
            float speed = displacement.Length() / duration;
            // Normalizing a zero vector will lead to issues
            if (displacement.LengthSquared() > 0.00001f)
                velocity = Vector2.Normalize(displacement) * speed;
            else
                velocity = Vector2.Zero;
            this.duration = duration;
            timer = 0;
        }
        /// <summary>
        /// Move the camera to the given location with the specified speed.
        /// </summary>
        /// <param name="position">Position to move to</param>
        /// <param name="speed">Speed of the movement in pixels per second.</param>
        public void MoveToWithSpeed(Vector2 position, float speed)
        {
            target = position;
            Vector2 displacement = position - this.position;
            float duration = displacement.Length() / speed;
            // Normalizing a zero vector will lead to issues
            if (displacement.LengthSquared() > 0.00001f)
                velocity = Vector2.Normalize(displacement) * speed;
            else
                velocity = Vector2.Zero;
            this.duration = duration;
            timer = 0;
        }

        /// <summary>
        /// Move the camera to the given screen in the specified time.
        /// </summary>
        /// <param name="screen">Screen to move to</param>
        /// <param name="duration">Seconds it takes for the move to complete.</param>
        public void FocusToInTime(IScreen screen, float duration)
        {
            MoveToInTime(screen.Position, duration);
        }
        /// <summary>
        /// Move the camera to the given location with the specified speed.
        /// </summary>
        /// <param name="screen">Screen to move to</param>
        /// <param name="speed">Speed of the movement in pixels per second.</param>
        public void FocusToWithSpeed(IScreen screen, float speed)
        {
            MoveToWithSpeed(screen.Position, speed);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix transformation, IEnumerable<IScreen> screens)
        {
            List<IScreen> screenList = screens.ToList();
            // Sort the screens such that the ones with greater layer will be later in the list and thus drawn higher.
            screenList.Sort(new LayerComparer());

            foreach (IScreen screen in screenList)
            {
                screen.Draw(gameTime, spriteBatch, GetTransform() * transformation);
            }

            if (InMotion)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (!InMotion)
                {
                    position = target;
                    MoveComplete?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    /// <summary>
    /// Class to help with sorting screens by layer.
    /// Should probably be moved to a better location.
    /// </summary>
    public class LayerComparer : IComparer<IScreen>
    {
        public int Compare(IScreen x, IScreen y)
        {
            return x.Layer - y.Layer;
        }
    }
}
