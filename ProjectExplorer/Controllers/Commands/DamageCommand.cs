using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Character;

namespace ProjectExplorer.Controllers.Commands
{
    public class DamageCommand : ICommand
    {
        private IPlayer player;

        public DamageCommand(IPlayer player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.Damage(0, Vector2.Zero);
        }
    }
}
