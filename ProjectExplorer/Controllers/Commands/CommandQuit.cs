using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class CommandQuit : ICommand
    {
        private readonly Game1 myGame;

        public CommandQuit(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.Exit();
        }
    }
}
