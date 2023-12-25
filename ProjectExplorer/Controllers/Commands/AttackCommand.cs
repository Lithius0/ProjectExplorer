using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Character;

namespace ProjectExplorer.Controllers.Commands
{
    public class AttackCommand : ICommand
    {
        private IPlayer player;

        public AttackCommand(IPlayer player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.UsePrimary();
        }
    }
}
