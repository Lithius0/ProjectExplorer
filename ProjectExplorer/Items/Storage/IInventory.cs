using ProjectExplorer.CharacterNS;
using System;
using System.Collections.Immutable;

namespace ProjectExplorer.Items.Storage
{
    public interface IInventory
    {
        /// <summary>
        /// Add a given item to the inventory.
        /// If given item is already in the inventory, it'll add more of it.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="amount">Amount to add, must be positive.</param>
        public void AddItem(IItem item, int amount = 1);
        /// <summary>
        /// Gets the amount of the given item in the inventory.
        /// </summary>
        /// <returns>Amount of given item in inventory, 0 if item is not in inventory.</returns>
        public int AmountOf(IItem item);
        /// <summary>
        /// Whether or not the item is in the inventory.
        /// An item that player has 0 of will return false.
        /// </summary>
        public bool HasItem(IItem item);
        /// <summary>
        /// Remove a certain number of a given item in the inventory.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="amount">Amount to remove, must be positive.</param>
        public void RemoveItem(IItem item, int amount = 1);

        /// <summary>
        /// Gets all items in the inventory.
        /// </summary>
        public IImmutableDictionary<IItem, int> GetItems();

        /// <summary>
        /// Clear all items in the inventory.
        /// </summary>
        public void Clear();

        public event EventHandler<InventoryModifiedEventArgs> Modified;
    }
    public class InventoryModifiedEventArgs : EventArgs
    {
        public IItem Item { get; set; }
        public int Amount { get; set; }
    }
}