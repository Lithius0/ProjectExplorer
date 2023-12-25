using ProjectExplorer.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectExplorer.Utility;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.DebugMode;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Class that handles collisions.
    /// This class also handles casting to ICollidable so that ObjectManager doesn't need to worry about it.
    /// </summary>
    public class CollisionManager
    {
        private readonly ISet<ICollidable> collidables;

        public CollisionManager()
        {
            collidables = new HashSet<ICollidable>();
        }

        /// <summary>
        /// Adds an object to the manager, if applicable
        /// </summary>
        public void Add(IGameObject obj)
        {
            if (obj is ICollidable collidable)
                collidables.Add(collidable);
        }

        /// <summary>
        /// Removes an object from the manager, if applicable
        /// </summary>
        public void Remove(IGameObject obj)
        {
            if (obj is ICollidable collidable)
                collidables.Remove(collidable);
        }

        public void RemoveAll()
        {
            collidables.Clear();
        }

        /// <summary>
        /// Draws debug boxes around the colliders if debug mode is on.
        /// </summary>
        public void DrawColliders(SpriteBatch spriteBatch)
        {
            foreach (ICollidable collidable in collidables)
            {
                DebugColliders.Instance.DrawCollider(spriteBatch, collidable);
            }
        }
        public void CheckCollisions()
        {
            // Changing to array to allow for sequential numerical access
            ICollidable[] collideableArray = collidables.ToArray();
            for (int i = 0; i < collideableArray.Length; i++)
            {
                // Start at i + 1 to prevent duplicate intersection and self-intersection
                for (int j = i + 1; j < collideableArray.Length; j++)
                {
                    ICollidable objA = collideableArray[i];
                    ICollidable objB = collideableArray[j];
                    if (objA.GetCollider().Intersects(objB.GetCollider()))
                    {
                        Rectangle intersection = Rectangle.Intersect(objA.GetCollider(), objB.GetCollider());
                        objA.GetCollisionHandler().Collide(objB, intersection);
                        objB.GetCollisionHandler().Collide(objA, intersection);
                    }
                }
            }
        }
        public Rectangle LastValidSpotBetween(ICollidable source, Rectangle stop, int steps = 4, CollisionGroup group = CollisionGroup.Tiles)
        {
            Rectangle start = source.GetCollider();

            // To avoid having to do checks across every single collider multiple times,
            // grab a small subset (assuming distance between start and stop is small) and check that.
            IList<Rectangle> subset = new List<Rectangle>();
            Rectangle union = Rectangle.Union(start, stop);
            foreach (ICollidable collidable in collidables)
            {
                Rectangle collider = collidable.GetCollider();
                if (collidable.Group == group && collidable != source && union.Intersects(collider) )
                {
                    subset.Add(collider);
                }
            }

            // Proper checking
            Rectangle lastValidSpot = start;
            for (int i = 1; i <= steps; i++)
            {
                Rectangle testLocation = Positioning.Lerp(start, stop, (float)i / steps);

                foreach (Rectangle collider in subset)
                {
                    if (testLocation.Intersects(collider))
                        return lastValidSpot;
                }

                // Nothing was hit, so update last valid spot.
                lastValidSpot = testLocation;
            }
            return lastValidSpot;
        }
    }
}
