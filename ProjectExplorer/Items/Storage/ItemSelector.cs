using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.Storage
{
    public class ItemSelector : IItemSelector
    {
        private const int RowLength = 4;

        private readonly ItemGroups primaries;
        private readonly IList<ItemGroups> secondaries;

        private readonly IInventory inventory;

        // These are cached to avoid checking the inventory every frame.
        // primary and selectables will be updated whenever the inventory is modified.
        private IItem primary;
        private readonly IDictionary<int, IItem> selectables;
        private int selection = 0;

        public int Selection { get { return selection; } }
        public IItem Primary { get { return primary; } }
        public IItem Secondary { get { return selectables[selection]; } }
        public IDictionary<int, IItem> Selectables { get { return selectables; } }

        public ItemSelector(IInventory inventory)
        {
            primaries = ItemGroups.Swords;
            secondaries = new List<ItemGroups>
            {
                // Row 1
                ItemGroups.Bows,
                ItemGroups.Bombs,
                ItemGroups.Empty,
                ItemGroups.Empty,
                // Row 2
                ItemGroups.Empty,
                ItemGroups.Empty,
                ItemGroups.Empty,
                ItemGroups.Empty,
            };
            selectables = new Dictionary<int, IItem>(secondaries.Count);
            this.inventory = inventory;
            inventory.Modified += OnInventoryModified;

            UpdateSelections();
        }

        private void OnInventoryModified(object sender, InventoryModifiedEventArgs e)
        {
            UpdateSelections();
        }

        private void UpdateSelections()
        {
            primary = primaries.BestItemIn(inventory);
            selectables.Clear();
            for (int i = 0; i < secondaries.Count; i++)
            {
                selectables[i] = secondaries[i].BestItemIn(inventory);
            }
        }

        public void MoveSecondarySelection(Direction direction)
        {
            int offset = direction switch
            {
                Direction.Up => -RowLength,
                Direction.Down => RowLength,
                Direction.Left => -1,
                Direction.Right => 1,
                _ => 1,
            };


            selection += offset;
            if (selection < 0)
                selection += selectables.Count;
            selection %= selectables.Count;
        }
    }
}
