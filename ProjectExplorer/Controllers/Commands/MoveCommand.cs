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
    public class MoveCommand : ICommand
    {
        private Direction direction;
        private IPlayer player;

        public MoveCommand(IPlayer player, Direction direction)
        {
            this.player = player;
            this.direction = direction;
        }

        public void Execute()
        {
            if (Coordinator.Instance.State == GameState.Inventory)
            {
                player.ItemSelector.MoveSecondarySelection(direction);
            }
            player.Move(direction);
        }
    }
}
