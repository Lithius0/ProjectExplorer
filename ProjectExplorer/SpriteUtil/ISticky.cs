using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.SpriteUtil
{
    /// <summary>
    /// Objects which implement this interface can have a sprite stuck to them.
    /// </summary>
    public interface ISticky
    {
        /// <summary>
        /// Position to stick to.
        /// </summary>
        public Vector2 Position { get; }
    }
}
