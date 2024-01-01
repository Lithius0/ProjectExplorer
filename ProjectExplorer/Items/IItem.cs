using ProjectExplorer.Character;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items
{
    /// <summary>
    /// Abstract representation of an item.
    /// For items which are "physical" objects in the game world, see <see cref="ItemEntities.ItemEntity"/>
    /// </summary>
    public interface IItem
    {
        public static IItem Instance { get; }
        public void Pickup(IPlayer player, int amount = 1);
        /// <summary>
        /// Uses the item
        /// </summary>
        /// <returns>True if the item was used successfully. False otherwise</returns>
        public bool Use(IPlayer player);

        /// <summary>
        /// Gets the sprite for this item.
        /// </summary>
        public SpriteDefinition GetSprite();
    }
}
