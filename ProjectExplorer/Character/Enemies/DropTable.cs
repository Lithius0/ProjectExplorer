using Microsoft.Xna.Framework;
using ProjectExplorer.Items;
using ProjectExplorer.Items.ItemEntities;
using ProjectExplorer.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Enemies
{
    public class DropTable
    {
        public static readonly DropTable Empty = new(1);

        private readonly IDictionary<IItem, int> weightTable;
        private readonly IDictionary<IItem, int> quantityTable;
        private int weightSum;
        private Random random;

        /// <summary>
        /// Creates a new drop table.
        /// </summary>
        /// <param name="emptyWeight">The weight of nothing being dropped.</param>
        public DropTable(int emptyWeight) 
        {
            weightSum = emptyWeight;
            random = new Random();
            weightTable = new Dictionary<IItem, int>();
            quantityTable = new Dictionary<IItem, int>();
        }

        public void Add(IItem item, int weight, int quantity = 1) 
        { 
            weightTable.Add(item, weight);
            weightSum += weight;
            quantityTable.Add(item, quantity);
        }

        public void Drop(ILevel level, Vector2 position)
        {
            int choice = random.Next(weightSum);
            IItem item = null;
            foreach (KeyValuePair<IItem, int> pair in weightTable)
            {
                choice -= pair.Value;
                if (choice < 0)
                {
                    item = pair.Key;
                    break;
                }
            }

            if (item != null)
            {
                level.Register(new ItemEntity(item, position, quantityTable[item]));
            }
            // If item is null, that means nothing has been chosen.
        }
    }
}
