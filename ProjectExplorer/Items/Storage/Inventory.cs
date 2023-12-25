using ProjectExplorer.Character;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Storage
{
    public class Inventory : IInventory
    {
        private readonly IDictionary<IItem, int> inventory;

        public event EventHandler<InventoryModifiedEventArgs> Modified;

        public Inventory()
        {
            inventory = new Dictionary<IItem, int>();
        }

        public void AddItem(IItem item, int amount = 1)
        {
            // Clamping, anything less than 1 is invalid
            int amountAdded = Math.Max(amount, 1);

            if (inventory.ContainsKey(item))
                inventory[item] += amountAdded;
            else
                inventory[item] = amountAdded;

            InventoryModifiedEventArgs args = new()
            {
                Item = item,
                Amount = amountAdded
            };
            Modified?.Invoke(this, args);
        }

        public void RemoveItem(IItem item, int amount = 1)
        {
            if (HasItem(item))
            {
                // Clamping, can't remove more than we have and can't remove less than 1.
                int amountRemoved = Math.Clamp(amount, 1, inventory[item]);
                inventory[item] -= amountRemoved;
                InventoryModifiedEventArgs args = new()
                {
                    Item = item,
                    Amount = -amount // Amount needs to be negative here to signify a removal
                };
                Modified?.Invoke(this, args);
            }
        }

        public void Clear()
        {
            // Doing this rather than just a clear call so that the event gets called.
            foreach(KeyValuePair<IItem, int> itemStack in inventory)
            {
                RemoveItem(itemStack.Key, itemStack.Value);
            }
        }

        public int AmountOf(IItem item)
        {
            if (inventory.ContainsKey(item))
                return inventory[item];
            else
                return 0;
        }

        public bool HasItem(IItem item)
        {
            return AmountOf(item) > 0;
        }

        public IImmutableDictionary<IItem, int> GetItems()
        {
            return inventory.ToImmutableDictionary();
        }
    }
}
