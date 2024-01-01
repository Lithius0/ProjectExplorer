using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Levels;
using ProjectExplorer.Character;
using ProjectExplorer.Character.Sprite;
using ProjectExplorer.Projectiles;
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
    public class StarterSword : IItem
    {
        private static readonly StarterSword instance = new();
        public static IItem Instance => instance;

        private IGameObject swordThrust = null;
        private StarterSword()
        {
        }
        
        public SpriteDefinition GetSprite()
        {
            return SpriteManager.GetSpriteDefinition("Items", new Rectangle(0, 16, 16, 16));
        }

        public void Pickup(IPlayer player, int amount = 1)
        {
            player.Inventory.AddItem(Instance, amount);
            player.PlayAnimation(PlayerSpriteFactory.Instance.GetPickupSprite(player.Position, Instance, 1, false), true);
        }

        public bool Use(IPlayer player)
        {
            if (!player.Level.IsRegistered(swordThrust))
            {
                Vector2 swordPosition = player.GetTransform().Center.ToVector2();

                swordThrust = new SwordSweep(player.Direction, player);

                player.Level.Register(swordThrust);

                if (player.Health >= player.MaxHealth)
                { 
                    // player.Level.Register(new SwordProjectile(swordPosition, player.Direction, player));
                }
                SoundFactory.Instance.PlaySound("SwordSlash");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "StarterSword";
        }
    }
}
