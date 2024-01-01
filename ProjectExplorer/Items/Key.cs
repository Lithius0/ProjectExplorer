using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ProjectExplorer.Character;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.SpriteUtil;

namespace ProjectExplorer.Items
{
    public class Key : IItem
    {
        private static readonly Key instance = new();
        public static IItem Instance => instance;
        private Key()
        {
        }

        public SpriteDefinition GetSprite()
        {
            return new SpriteDefinition("Items", 0, 64, 16, 16);
        }

        public void Pickup(IPlayer player, int amount = 1)
        {
            player.Inventory.AddItem(Instance, amount);
        }

        public bool Use(IPlayer player)
        {
            return false;
        }

        public override string ToString()
        {
            return "Key";
        }
    }
}
