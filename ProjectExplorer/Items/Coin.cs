using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Character;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items
{
    public class Coin : IItem
    {
        private static readonly Coin instance = new();
        public static IItem Instance => instance;
        private Coin()
        {
        }

        public SpriteDefinition GetSprite()
        {
            return SpriteManager.GetSpriteDefinition("Objects", new Rectangle(0, 64, 16, 16));
        }

        public void Pickup(IPlayer player, int amount = 1)
        {
            player.Inventory.AddItem(Instance, amount);
            SoundFactory.Instance.PlaySound("GetCoin");
        }

        public bool Use(IPlayer player)
        {
            return false;
        }

        public override string ToString()
        {
            return "Coin";
        }
    }
}
