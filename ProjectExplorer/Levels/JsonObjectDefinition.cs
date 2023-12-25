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
    public class JsonObjectDefinition : ObjectDefinition
    {
        private JsonNode jsonNode;

        protected static T GetValue<T>(JsonObject jsonObject, string key, T def)
        {
            if (jsonObject.ContainsKey(key))
                // We want the default to be returned only in case of a missing entry.
                // Invalid type should throw an exception.
                return jsonObject[key].GetValue<T>(); 
            else
                return def;
        }

        public JsonObjectDefinition(JsonNode jsonNode)
        {
            this.jsonNode = jsonNode;

            JsonObject jsonObject = jsonNode.AsObject();

            ObjectId = (string)jsonObject["ObjectId"];
            jsonObject.Remove("ObjectId");

            // Position parsing
            Position = new Vector2(0, 0); // Origin
            // The f's are important for the compiler to know that these are floats.
            Position += new Vector2(GetValue(jsonObject, "TileX", 0f), GetValue(jsonObject, "TileY", 0f)) * Tiling.Full.ToVector2();
            Position += new Vector2(GetValue(jsonObject, "X", 0f), GetValue(jsonObject, "Y", 0f)); // Pixel offsets
            jsonObject.Remove("TileX");
            jsonObject.Remove("TileY");
            jsonObject.Remove("X");
            jsonObject.Remove("Y");

            List<string> tagsList = new();
            if (jsonNode[nameof(Tags)] != null)
            {
                JsonArray tags = jsonNode[nameof(Tags)].AsArray();
                foreach (JsonNode node in tags)
                {
                    tagsList.Add((string)node);
                }
            }
            Tags = tagsList.ToArray();
            jsonObject.Remove("Tags");
        }

        public override bool ContainsKey(string key)
        {
            return jsonNode[key] != null;
        }

        public override T GetValue<T>(string key, T def)
        {
            if (jsonNode[key] != null)
                return jsonNode[key].GetValue<T>();
            else
                return def;
        }

        public override T GetValue<T>(string key)
        {
            return jsonNode[key].GetValue<T>();
        }
    }
}
