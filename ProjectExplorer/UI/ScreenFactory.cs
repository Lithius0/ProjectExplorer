using ProjectExplorer.Levels;
using ProjectExplorer.Character;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.UI
{
    public class ScreenFactory
    {
        private static ScreenFactory instance = new();
        public static ScreenFactory Instance => instance;

        private ScreenFactory() { }


        public IScreen CreateInventoryHud(Vector2 position, LevelManager levelManager)
        {
            IScreen inventoryHud = new Screen(position, 10);
            inventoryHud.AddModule(new InventoryDisplay(levelManager.Player, levelManager));
            return inventoryHud;
        }
    }
}
