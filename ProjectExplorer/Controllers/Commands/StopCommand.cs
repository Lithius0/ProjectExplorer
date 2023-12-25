using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.Utility;

namespace ProjectExplorer.Controllers.Commands
{
    public class StopCommand : ICommand
    {
        private Direction direction;
        private IPlayer player;

        public StopCommand(IPlayer player, Direction direction)
        {
            this.player = player;
            this.direction = direction;
        }

        public void Execute()
        {
            player.Stop(direction);
        }
    }
}
