using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Levels;
using ProjectExplorer.Character;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items
{
    public class Bomb : IItem
    {
        private static readonly Bomb instance = new();
        public static IItem Instance => instance;
        private Bomb()
        {
        }

        public SpriteDefinition GetSprite()
        {
            return new SpriteDefinition("Items", 0, 0, 16, 16);
        }

        public void Pickup(IPlayer player, int amount = 1)
        {
            player.Inventory.AddItem(Instance, amount);
        }

        public bool Use(IPlayer player)
        {
            if (player.Inventory.HasItem(Instance))
            {
                Vector2 target = player.GetTransform().Center.ToVector2() + player.Direction.ToVector2() * Tiling.TileLength;
                player.Level.Register(new PlacedBomb(2, target));
                SoundFactory.Instance.PlaySound("BombDrop");
                player.Inventory.RemoveItem(Instance);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "Bomb";
        }
    }
}
