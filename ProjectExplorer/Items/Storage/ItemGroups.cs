using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Storage
{
    /// <summary>
    /// ItemGroups are items which overlap each other in the inventory. 
    /// Having a higher priority one doesn't remove the lower one but the higher one gets selected first.
    /// Items which are earlier in their list have higher priority.
    /// </summary>
    public class ItemGroups
    {
        // Despite being plural, most of these have only one item.
        // That's intentional to improve scalability.
        public static readonly ItemGroups Swords = new(new List<IItem>
        {
            // Add the other swords in here
            StarterSword.Instance,
        });
        //public static readonly ItemGroups Boomerangs = new(Boomerang.Instance);
        public static readonly ItemGroups Bombs = new(Bomb.Instance);
        public static readonly ItemGroups Bows = new(Bow.Instance);
        public static readonly ItemGroups Empty = new();

        private readonly IList<IItem> items;

        public ItemGroups(IList<IItem> items)
        {
            this.items = items;
        }
        public ItemGroups(IItem[] items)
        {
            this.items = items.ToArray();
        }
        public ItemGroups(IItem item)
        {
            items = new List<IItem>
            {
                item
            };
        }
        public ItemGroups()
        {
            items = new List<IItem>();
        }
        public IItem BestItemIn(IInventory inventory)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (inventory.HasItem(items[i]))
                {
                    return items[i];
                }
            }
            return null;
        }
    }
}
