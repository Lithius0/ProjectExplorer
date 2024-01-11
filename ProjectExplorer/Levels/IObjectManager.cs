using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Any object which manages a collection of IGameObjects.
    /// It allows an objects to manage its own lifecycle without aid from the creator.
    /// Allows objects from both above and below to create and register new objects and forget about them.
    /// Also allows for mass deregistration for easy clearing of similar objects.
    /// </summary>
    public interface IObjectManager
    {
        /// <summary>
        /// Registers a IGameObject with the manager which will be updated and drawn every frame
        /// </summary>
        /// <param name="obj">The object to register</param>
        public void Register(IGameObject obj);

        /// <summary>
        /// Once an object no longer needs to be updated or drawn, call this method to remove it from the manager.
        /// An object that isn't registered should not throw an exception. It'll just do nothing.
        /// </summary>
        public void Deregister(IGameObject obj);

        /// <summary>
        /// Deregisters all registered game objects.
        /// </summary>
        public void DeregisterAll();

        /// <summary>
        /// Distributes the update call to held objects.
        /// </summary>
        public void Update(GameTime gameTime);

        /// <summary>
        /// Distributes the draw call to held objects.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Checks whether or not the given object is registered.
        /// Null always returns false.
        /// </summary>
        /// <returns>True if object given is registered, false otherwise or if obj is null</returns>
        public bool IsRegistered(IGameObject obj);

        /// <summary>
        /// Gets all of the objects in this manager which are of type T
        /// </summary>
        public ISet<T> GetObjects<T>() where T : IGameObject;

        /// <summary>
        /// Add a child to an object. When the parent is deregistered, the child will be as well.
        /// Each child can only have one parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void AddChild(IGameObject parent, IGameObject child);
    }
}
