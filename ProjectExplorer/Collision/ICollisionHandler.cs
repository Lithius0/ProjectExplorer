using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    public interface ICollisionHandler
    { 
        public void Collide(ICollidable other, Rectangle intersection);
    }
}
