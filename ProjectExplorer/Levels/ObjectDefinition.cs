using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json;
using System;
using ProjectExplorer.Items;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Security;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Class which decribes a intermediary definition for all objects between the map file and C# objects.
    /// All fields read are stored in a string-value dictionary. 
    /// The important fields have properties within this class.
    /// TODO: Make this serializable back into JSON
    /// </summary>
    public abstract class ObjectDefinition
    {
        public string ObjectId { get; set; }
        public Vector2 Position { get; set; }
        public string[] Tags { get; set; }


        public abstract T GetValue<T>(string key, T def);
        public abstract T GetValue<T>(string key);

        public Vector2 GetVector2(string keyX, string keyY, Vector2? def = null) 
        { 
            if (def.HasValue)
            {
                return new Vector2(GetValue(keyX, def.Value.X), GetValue(keyY, def.Value.Y));
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
        public Point GetPoint(string keyX, string keyY, Point? def = null)
        {
            if (def.HasValue)
            {
                return new Point(GetValue(keyX, def.Value.X), GetValue(keyY, def.Value.Y));
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public abstract bool ContainsKey(string key);
    }
}
