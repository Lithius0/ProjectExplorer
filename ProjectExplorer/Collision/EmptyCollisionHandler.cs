using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    /// <summary>
    /// A class to handle all the collisions that do nothing.
    /// </summary>
    public class EmptyCollisionHandler : ICollisionHandler
    {
        private static readonly EmptyCollisionHandler instance = new();
        public static EmptyCollisionHandler Instance
        {
            get { return instance; }
        }

        private EmptyCollisionHandler() { }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            
        }
    }
}
