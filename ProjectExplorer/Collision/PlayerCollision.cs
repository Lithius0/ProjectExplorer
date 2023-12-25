using ProjectExplorer.Items;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    public class PlayerCollision : ICollisionHandler
    {
        private IPlayer player;

        public PlayerCollision(IPlayer player)
        {
            this.player = player;
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            if (other is IDoor door && player.Inventory.HasItem(Key.Instance) 
                && !door.IsOpen() && door.OpensWithAnyKey)
            {
                door.Toggle();
                player.Inventory.RemoveItem(Key.Instance);
            }
        }
    }
}
