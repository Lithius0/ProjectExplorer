using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Utility
{
    /// <summary>
    /// Whole bunch of methods to get a random value of something.
    /// Used for enemy movement and the like.
    /// </summary>
    public static class Randomizer
    {
        private static Random r = new Random();
        private static readonly float root2 = MathF.Sqrt(2);

        // Vectors pointing in the 8 standard compass directions
        public static readonly Vector2[] CompassVectors =
        {
            new Vector2(0, -1), // N
            new Vector2(1, 0), // E
            new Vector2(0, 1), // S 
            new Vector2(-1, 0), // W
            new Vector2(1, -1), // NE
            new Vector2(1, 1), // SE
            new Vector2(-1, 1), // SW
            new Vector2(-1, -1), // NW
        };

        /// <summary>
        /// Gets a random unit vector pointing in one of eight standard compass directions.
        /// <i>Diagonals are not normalized.</i>
        /// </summary>
        /// <returns>A random vector along the compass directions</returns>
        public static Vector2 RandomCompassVector()
        {
            return CompassVectors[r.Next(8)];
        }

        /// <summary>
        /// Gets a random unit vector pointing either N, E, S, or W.
        /// </summary>
        /// <returns>A random unit vector</returns>
        public static Vector2 RandomCardinalVector()
        {
            return CompassVectors[r.Next(4)];
        }

        /// <summary>
        /// Fully random unit vector, pointing in any direction
        /// Circular distribution, i.e. all angles are equally likely
        /// </summary>
        /// <returns>A random unit vector</returns>
        public static Vector2 RandomUnitVector()
        {
            double angle = r.NextDouble() * Math.Tau;
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
