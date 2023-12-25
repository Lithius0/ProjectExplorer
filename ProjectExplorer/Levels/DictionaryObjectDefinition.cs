using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    public class DictionaryObjectDefinition : ObjectDefinition
    {
        private IDictionary<string, object> fields;

        public DictionaryObjectDefinition(string objectId, Vector2 position, string[] tags) {
            fields = new Dictionary<string, object>();
            ObjectId = objectId;
            Position = position;
            Tags = tags;
        }

        public override bool ContainsKey(string key)
        {
            return fields.ContainsKey(key);
        }

        public override T GetValue<T>(string key, T def)
        {
            if (fields.ContainsKey(key))
            {
                // TODO: Slightly more sophisticated error handling
                return (T)fields[key];
            }
            else
            {
                return def;
            }
        }

        public override T GetValue<T>(string key)
        {
            return (T)fields[key];
        }
    }
}
