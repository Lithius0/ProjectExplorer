using ProjectExplorer.Tiles;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Runtime.CompilerServices;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Responsible for loading levels into the level manager from files.
    /// </summary>
    public class LevelLoader
    {
        private static string directory = @"LevelFiles\";

        public static IDictionary<string, ILevel> LoadAll(LevelManager manager)
        {
            return LoadAll(directory, manager);
        }

        public static IDictionary<string, ILevel> LoadAll(string root, LevelManager manager)
        {
            IDictionary<string, ILevel> levels = new Dictionary<string, ILevel>();
            LoadAll(root, manager, ref levels);
            return levels;
        }

        public static void LoadAll(string root, LevelManager manager, ref IDictionary<string, ILevel> levels)
        {
            foreach (string filename in Directory.EnumerateFiles(root))
            {
                ILevel level = LoadLevel(filename, manager);
                levels.Add(level.LevelId, level);
            }
            foreach (string dir in Directory.EnumerateDirectories(root))
            {
                LoadAll(dir, manager, ref levels);
            }
        }

        public static Level LoadLevel(string filepath, LevelManager manager)
        {
            JsonDocumentOptions options = new();
            options.AllowTrailingCommas = true;

            JsonNode forecastNode = JsonNode.Parse(File.ReadAllText(filepath), null, options);

            // Levels are relatively simple, so they're deserialized with a class definition.
            LevelDefinition levelDef = forecastNode.Deserialize<LevelDefinition>();
            Level level = new(levelDef.LevelId, manager, levelDef.GetMapPosition());

            for (int row = 0; row < levelDef.Tiles.Length; row++)
            {
                string rowString = levelDef.Tiles[row];
                for (int column = 0; column < rowString.Length; column++)
                {
                    char tileChar = rowString[column];
                    if (tileChar != ' ')
                    {
                        string tileId = levelDef.Tileset[tileChar];
                        Vector2 position = new Vector2(column, row) * Tiling.Full.ToVector2();
                        ObjectDefinition tileDef = new DictionaryObjectDefinition(tileId, position, Array.Empty<string>());
                        level.Register(ObjectRegistry.GetObject(tileDef));
                    }
                }
            }

            JsonArray objectArray = forecastNode["Objects"].AsArray();
            foreach (JsonNode objectNode in objectArray)
            {
                ObjectDefinition objectDef = new JsonObjectDefinition(objectNode);
                IGameObject obj = ObjectRegistry.GetObject(objectDef);
                level.Register(obj);

                // Applying all the tags.
                foreach (string tag in objectDef.Tags)
                {
                    level.Tag(obj, tag);
                }

            }

            return level;
        }
    }
}
