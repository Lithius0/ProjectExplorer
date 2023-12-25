using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// This interface is used for deserializing objects from their definitions.
    /// A template object implementing IMitotic is created and stored by the deserializer.
    /// The ObjectDefinition then dictates what parts to override for customization.
    /// </summary>
    public interface IMitotic
    {
        /// <summary>
        /// Clones an instance of the object with the given object definition.
        /// </summary>
        /// <param name="objectDefinition"></param>
        /// <returns></returns>
        public IGameObject Clone(ObjectDefinition objectDefinition);
    }
}
