using ProjectExplorer.CharacterNS;
using ProjectExplorer.Tiles;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    /// <summary>
    /// Collision handler for tiles. Pushes characters out when they're detected.
    /// <br />
    /// <b>Assumes the character can only move orthogonally.</b>
    /// </summary>
    public class TileCollisionHandler : ICollisionHandler
    {
        public virtual void Collide(ICollidable other, Rectangle intersection)
        {
            if (other is IExclusion exclusion)
            {
                exclusion.Exclude(intersection);
            }
        }
    }
}
