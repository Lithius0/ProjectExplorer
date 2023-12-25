using ProjectExplorer.Items.Sprites;
using ProjectExplorer.CharacterNS;
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
    public class HeartContainer : IItem
    {
        private static readonly HeartContainer instance = new();
        public static IItem Instance => instance;
        private HeartContainer()
        {
        }

        public ISprite GetSprite(Vector2 position)
        {
            return ItemSpriteFactory.Instance.GetHeartContainerSprite(position);
        }

        public void Pickup(IPlayer player, int amount)
        {
            player.AddMaxHealth(amount);
            player.Heal(amount);
            SoundFactory.Instance.PlaySound("GetHeart");
        }

        public bool Use(IPlayer player)
        {
            return false;
        }

        public override string ToString()
        {
            return "HeartContainer";
        }
    }
}
