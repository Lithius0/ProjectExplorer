using ProjectExplorer.CharacterNS;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.DebugMode
{
    public class PlayerInfo : InfoModule
    {
        private IPlayer player;

        public PlayerInfo(IPlayer player, Vector2 position)
        {
            this.player = player;
            Name = "Player";
            Position = position;
        }

        protected override void UpdateInfo()
        {
            sections["Health"] = player.Health.ToString();
            sections["Position"] = $"({player.Position.X : 0.0}, {player.Position.Y: 0.0})";
            sections["Direction"] = player.Direction.ToString();
            sections["State"] = player.StateMachine.State.ToString();
            sections["Primary"] = player.ItemSelector.Primary?.ToString();
            sections["Secondary"] = player.ItemSelector.Secondary?.ToString();
        }
    }
}
