using ProjectExplorer.DebugMode;
using ProjectExplorer.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class DebugCommand : ICommand
    {
        public DebugCommand()
        {
        }

        public void Execute()
        {
            Coordinator.Instance.ToggleDebug();
            DebugColliders.Instance.Toggle();
        }
    }
}
