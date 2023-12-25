using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.UI;

namespace ProjectExplorer.Controllers.Commands
{
    public class ZoomCommand : ICommand
    {
        private int amount;
        private ScreenManager screen;
        public ZoomCommand(ScreenManager screen, int amount)
        {
            this.amount = amount;
            this.screen = screen;
        }

        public void Execute()
        {
            screen.Scale += amount;
        }
    }
}
