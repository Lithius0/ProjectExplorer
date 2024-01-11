using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    public abstract class ObjectManager : IObjectManager
    {
        protected readonly IDictionary<IGameObject, ObjectNode> objectMap = new Dictionary<IGameObject, ObjectNode>();
        public virtual void Deregister(IGameObject obj)
        {
            if (objectMap.ContainsKey(obj))
            {
                Deregister(objectMap[obj]);
            }
        }

        protected void Deregister(ObjectNode node)
        {
            foreach (ObjectNode child in node.Children)
            {
                // Recursively deregister all children as well.
                Deregister(child);
            }
            node.ClearChildren();
            objectMap.Remove(node.Value);
        }

        public virtual void DeregisterAll()
        {
            objectMap.Clear();
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public virtual ISet<T> GetObjects<T>() where T : IGameObject
        {
            ISet<T> subset = new HashSet<T>();
            foreach (IGameObject obj in objectMap.Keys)
            {
                if (obj is T t)
                    subset.Add(t);
            }
            return subset;
        }

        public virtual bool IsRegistered(IGameObject obj)
        {
            // gameObjects shouldn't contain null anyways. This is a second check after Register's null check.
            if (obj == null)
                return false;
            else
                return objectMap.ContainsKey(obj);
        }

        public virtual void Register(IGameObject obj)
        {
            if (obj != null)
            {
                objectMap.Add(obj, new ObjectNode(obj));
            }
            else
            {
                throw new ArgumentNullException(nameof(obj), "Cannot register a null object!");
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            // Changing to array to prevent a concurrent modification issue
            // For example, if an object deregisters itself during update.
            foreach (IGameObject gameObject in objectMap.Keys.ToArray())
            {
                gameObject.Update(gameTime);
            }
        }

        public virtual void AddChild(IGameObject parent, IGameObject child)
        {
            if (objectMap.ContainsKey(parent) && objectMap.ContainsKey(child))
            {
                objectMap[parent].AddChild(objectMap[child]);
            }
        }
    }
}
