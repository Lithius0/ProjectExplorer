using ProjectExplorer.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class SelectCommand : ICommand
    {
        private IPlayer player;
        private Direction direction;
        public SelectCommand(IPlayer player, Direction direction)
        {
            this.player = player;
            this.direction = direction;
        }

        public void Execute()
        {
            if (Coordinator.Instance.State == GameState.Inventory)
                player.ItemSelector.MoveSecondarySelection(direction);
        }
    }
}
