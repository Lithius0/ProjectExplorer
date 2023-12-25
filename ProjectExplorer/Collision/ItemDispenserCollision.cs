using ProjectExplorer.Items.ItemEntities;
using ProjectExplorer.Character;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    /// <summary>
    /// Collision handler for any object which gives the player an item
    /// </summary>
    public class ItemDispenserCollision : ICollisionHandler
    {
        private IItemDispenser itemDispenser;

        public ItemDispenserCollision(IItemDispenser itemDispenser)
        {
            this.itemDispenser = itemDispenser;
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            if (other is IPlayer player)
            {
                itemDispenser.Dispense(player);
            }
        }
    }
}
