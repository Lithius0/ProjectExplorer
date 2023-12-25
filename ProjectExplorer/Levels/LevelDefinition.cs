using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{


    /// <summary>
    /// JSON Definition class for the level. Used for the deserializer.
    /// </summary>
    public class LevelDefinition
    {
        protected JsonNode jsonNode;

        public string LevelId { get; set; }
        public int MapX { get; set; }
        public int MapY { get; set; }
        public IDictionary<char, string> Tileset { get; set; }
        public string[] Tiles { get; set; }

        public Point GetMapPosition()
        {
            return new Point(MapX, MapY);
        }
    }
}
