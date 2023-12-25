using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Utility
{
    /// <summary>
    /// Utility class to help with converting tiles to pixels.
    /// </summary>
    public static class Tiling
    {
        public static readonly int TileLength = 16;
        // Sizes
        public static readonly Point Full = new(TileLength);
        public static readonly Point HalfVertical = new(TileLength / 2, TileLength);
        public static readonly Point HalfHorizontal = new(TileLength, TileLength / 2);
        public static readonly Point Quarter = new(TileLength / 2, TileLength / 2);
        public static readonly Point LevelSize = new(TileLength * 32, TileLength * 14);

        /// <summary>
        /// Converts tile lengths to pixel lengths.
        /// Also works to convert speeds from tiles/second to pixels/second
        /// </summary>
        /// <param name="tiles">Distance in tiles</param>
        /// <returns>Distance in pixels</returns>
        public static int ToPixels(float tiles)
        {
            return (int)(tiles * TileLength);
        }

        public static Point ToPixels(float tilesX, float tilesY)
        {
            return new Point((int)(tilesX * TileLength), (int)(tilesY * TileLength));  
        }
    }
}
