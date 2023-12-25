﻿using ProjectExplorer.Items.Sprites;
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
    public class Heart : IItem
    {
        private static readonly Heart instance = new();
        public static IItem Instance => instance;
        private Heart()
        {
        }

        public ISprite GetSprite(Vector2 position)
        {
            return ItemSpriteFactory.Instance.GetHeartSprite(position).Play();
        }

        public void Pickup(IPlayer player, int amount = 1)
        {
            player.Heal(amount * 2);
            SoundFactory.Instance.PlaySound("RefillLoop");
        }

        public bool Use(IPlayer player)
        {
            // Heart should never actually be in a player inventory.
            return false;
        }

        public override string ToString()
        {
            return "Heart";
        }
    }
}
