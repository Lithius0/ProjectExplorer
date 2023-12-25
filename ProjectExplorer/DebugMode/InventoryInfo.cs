using ProjectExplorer.Items;
using ProjectExplorer.Items.Storage;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.DebugMode
{
    public class InventoryInfo : InfoModule
    {
        private readonly IInventory inventory;

        public InventoryInfo(IInventory inventory, Vector2 position) 
        {
            this.inventory = inventory;
            Name = "Inventory";
            Position = position;
        }

        protected override void UpdateInfo()
        {
            IImmutableDictionary<IItem, int> inventoryCopy = inventory.GetItems();
            foreach(KeyValuePair<IItem, int> entry in inventoryCopy) 
            {
                sections[entry.Key.ToString()] = entry.Value.ToString();
            }
        }
    }
}
