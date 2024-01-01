using Microsoft.Xna.Framework;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Character;
using ProjectExplorer.Projectiles;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items
{
    public class Bow : IItem
    {
        private static readonly Bow instance = new();
        public static IItem Instance => instance;
        private Bow()
        {
        }

        public void Pickup(IPlayer player, int amount = 1)
        {
            player.Inventory.AddItem(Instance, amount);
        }

        public bool Use(IPlayer player)
        {
            if (player.Inventory.HasItem(Coin.Instance))
            {
                Vector2 direction = player.Direction.ToVector2();
                Vector2 arrowPosition = player.GetTransform().Center.ToVector2() + direction * Tiling.TileLength;
                player.Level.Register(new Arrow(arrowPosition, direction * Tiling.ToPixels(16), player, 0.5f));
                SoundFactory.Instance.PlaySound("ArrowBoomerang");
                player.Inventory.RemoveItem(Coin.Instance);
                return true;
            }
            return false;
        }

        public SpriteDefinition GetSprite()
        {
            return new SpriteDefinition("Items", 0, 96, 16, 16);
        }

        public override string ToString()
        {
            return "Bow";
        }
    }
}
