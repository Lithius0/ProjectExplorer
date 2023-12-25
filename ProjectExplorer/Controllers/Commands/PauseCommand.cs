using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.UI;

namespace ProjectExplorer.Controllers.Commands
{
    public class PauseCommand : ICommand
    {
        public void Execute()
        {
            Coordinator.Instance.TogglePause();
        }
    }
}
