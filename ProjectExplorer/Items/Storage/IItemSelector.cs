using System.Collections.Generic;

namespace ProjectExplorer.Items.Storage
{
    public interface IItemSelector
    {
        /// <summary>
        /// The index of the selected item.
        /// </summary>
        public int Selection { get; }
        /// <summary>
        /// The sword player is holding.
        /// </summary>
        public IItem Primary { get; }
        /// <summary>
        /// The selected item being used by player.
        /// </summary>
        public IItem Secondary { get; }
        /// <summary>
        /// All the items in player's inventory which can be selected, listed in order.
        /// </summary>
        public IDictionary<int, IItem> Selectables { get; }

        /// <summary>
        /// Moves the secondary selector around.
        /// </summary>
        public void MoveSecondarySelection(Direction direction);
    }
}