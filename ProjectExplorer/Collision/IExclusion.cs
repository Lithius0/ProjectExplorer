using Microsoft.Xna.Framework;

namespace ProjectExplorer.Collision
{
    /// <summary>
    /// This is just a hitbox which can be excluded from a wall.
    /// </summary>
    public interface IExclusion
    {
        /// <summary>
        /// When called, the excluded object will be move such that it moves out of the intersection.
        /// </summary>
        /// <param name="intersection"></param>
        public void Exclude(Rectangle intersection);
    }
}