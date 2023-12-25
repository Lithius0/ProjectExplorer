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

        public ISprite GetSprite(Vector2 position)
        {
            return ItemSpriteFactory.Instance.GetKeySprite(position);
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
