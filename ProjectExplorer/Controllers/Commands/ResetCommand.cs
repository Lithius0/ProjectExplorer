using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class ResetCommand : ICommand
    {
        public void Execute()
        {
            Coordinator.Instance.Reset();
        }
    }
}
