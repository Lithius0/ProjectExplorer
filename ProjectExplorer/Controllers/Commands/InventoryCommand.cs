using ProjectExplorer.Character;
using ProjectExplorer.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class InventoryCommand : ICommand
    {
        public void Execute()
        {
            Coordinator.Instance.ToggleInventory();
        }
    }
}
