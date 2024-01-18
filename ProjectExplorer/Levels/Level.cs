using ProjectExplorer.Collision;
using ProjectExplorer.Character;
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
    public class Level : ObjectManager, ILevel
    {
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

            collisionManager = new CollisionManager();
            tags = new Dictionary<string, ISet<IGameObject>>();

            Manager = manager;
        }

        public string LevelId => levelId;
        public Point MapPosition => mapPosition;

        public override void Deregister(IGameObject obj)
        {
            collisionManager.Remove(obj);
            base.Deregister(obj);
        }

        public override void DeregisterAll()
        {
            collisionManager.RemoveAll();
            base.DeregisterAll();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
            foreach (IGameObject gameObject in objectMap.Keys.ToArray())
            {
                gameObject.Draw(gameTime, spriteBatch);
            }
            collisionManager.DrawColliders(spriteBatch);
            spriteBatch.End();
        }


        public override void Register(IGameObject obj)
        {
            if (obj != null)
            {
                collisionManager.Add(obj);
                base.Register(obj);
                obj.OnRegister(this);
            }
            else
            {
                throw new ArgumentNullException(nameof(obj), "Cannot register a null object!");
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            collisionManager.CheckCollisions();
        }

        public Rectangle LastValidSpotBetween(ICollidable source, Rectangle end)
        {
            return collisionManager.LastValidSpotBetween(source, end);
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
