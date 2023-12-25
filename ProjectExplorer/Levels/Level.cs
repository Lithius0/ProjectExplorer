using ProjectExplorer.Collision;
using ProjectExplorer.CharacterNS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Class responsible for holding all the objects being updated and drawn.
    /// </summary>
    public class Level : ILevel
    {
        private readonly ISet<IGameObject> gameObjects;
        private readonly IDictionary<string, ISet<IGameObject>> tags;
        private readonly CollisionManager collisionManager;

        private string levelId;
        private Point mapPosition;

        public event EventHandler OnLoad;
        public event EventHandler OnUnload;

        public LevelManager Manager { get; }

        public Level(string levelId, LevelManager manager, Point mapPosition)
        {
            this.levelId = levelId;
            this.mapPosition = mapPosition;

            gameObjects = new HashSet<IGameObject>();
            collisionManager = new CollisionManager();
            tags = new Dictionary<string, ISet<IGameObject>>();

            Manager = manager;
        }

        public string LevelId => levelId;
        public Point MapPosition => mapPosition;

        public void Deregister(IGameObject obj)
        {
            gameObjects.Remove(obj);
            collisionManager.Remove(obj);
        }

        public void DeregisterAll()
        {
            gameObjects.Clear();
            collisionManager.RemoveAll();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Draw(gameTime, spriteBatch, Matrix.Identity);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix matrix)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.FrontToBack,
                transformMatrix: matrix
            );
            foreach (IGameObject gameObject in gameObjects.ToArray())
            {
                gameObject.Draw(gameTime, spriteBatch);
            }
            collisionManager.DrawColliders(spriteBatch);
            spriteBatch.End();
        }

        public bool IsRegistered(IGameObject obj)
        {
            // gameObjects shouldn't contain null anyways. This is a second check after Register's null check.
            if (obj == null)
                return false;
            else
                return gameObjects.Contains(obj);
        }

        public void Register(IGameObject obj)
        {
            if (obj != null)
            {
                gameObjects.Add(obj);
                collisionManager.Add(obj);
                obj.OnRegister(this);
            }
            else
            {
                throw new ArgumentNullException(nameof(obj), "Cannot register a null object!");
            }
        }

        public void Update(GameTime gameTime)
        {
            // Changing to array to prevent a concurrent modification issue
            // For example, if an object deregisters itself during update.
            foreach (IGameObject gameObject in gameObjects.ToArray())
            {
                gameObject.Update(gameTime);
            }
            collisionManager.CheckCollisions();
        }

        public Rectangle LastValidSpotBetween(ICollidable source, Rectangle end)
        {
            return collisionManager.LastValidSpotBetween(source, end);
        }

        public ISet<T> GetObjects<T>() where T : IGameObject
        {
            ISet<T> subset = new HashSet<T>();
            foreach(IGameObject obj in gameObjects)
            {
                if (obj is T t)
                    subset.Add(t);
            }
            return subset;
        }
        public IGameObject[] GetObjectsWithTag(string tag)
        {
            IGameObject[] taggedObjects = Array.Empty<IGameObject>();
            if (tags.ContainsKey(tag))
                taggedObjects = tags[tag].ToArray();

            return taggedObjects;
        }

        public void Load()
        {
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        public void Unload()
        {
            OnUnload?.Invoke(this, EventArgs.Empty);
        }

        public void Tag(IGameObject obj, string tag)
        {
            if (!tags.ContainsKey(tag))
                tags.Add(tag, new HashSet<IGameObject>());

            tags[tag].Add(obj);
        }
    }
}
