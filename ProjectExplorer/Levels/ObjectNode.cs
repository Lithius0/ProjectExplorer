using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    public class ObjectNode
    {
        private readonly IGameObject value;
        private ISet<ObjectNode> children;
        private ObjectNode parent;
        public IGameObject Value => value;
        public ObjectNode Parent => parent;
        public IEnumerable<ObjectNode> Children => children;

        public ObjectNode(IGameObject value)
        {
            this.value = value;
            parent = null;
            children = new HashSet<ObjectNode>();
        }

        public void AddChild(ObjectNode child)
        {
            children.Add(child);
            child.parent = this;
            // TODO: Should really try and prevent cycles.
        }
        public bool HasChild(ObjectNode node)
        {
            return children.Contains(node);
        }

        public void RemoveChild(ObjectNode node)
        {
            children.Remove(node);
            node.parent = null;
        }

        public void ClearChildren()
        {
            foreach (ObjectNode child in children)
            {
                RemoveChild(child);
            }
        }
    }
}
