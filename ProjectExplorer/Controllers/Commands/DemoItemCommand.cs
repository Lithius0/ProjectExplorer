using ProjectExplorer.CharacterNS;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    //Temporary command to allow Player to use an item based off a number
    public class DemoItemCommand : ICommand
    {
        private IPlayer player;
        private int location;
        public DemoItemCommand(IPlayer player, int location)
        {
            this.player = player;
            this.location = location;
        }

        public void Execute()
        {
            player.UseItem(location);
        }
    }
}
