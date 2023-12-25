using ProjectExplorer.CharacterNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class UseSecondaryCommand : ICommand
    {
        private IPlayer player;

        public UseSecondaryCommand(IPlayer player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.UseSecondary();
        }
    }
}
