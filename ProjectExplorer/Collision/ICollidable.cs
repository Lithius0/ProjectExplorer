using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    /// <summary>
    /// Objects that utilize collision logic need to implement this class.
    /// </summary>
    public interface ICollidable
    {
        public Rectangle GetCollider();
        public ICollisionHandler GetCollisionHandler();
        public CollisionGroup Group { get; }
    }
}
