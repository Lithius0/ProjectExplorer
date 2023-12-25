using ProjectExplorer.CharacterNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.ItemEntities
{
    public interface IItemDispenser
    {
        public void Dispense(IPlayer player);
    }
}
