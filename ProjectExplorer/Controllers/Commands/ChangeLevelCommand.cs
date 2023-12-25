using ProjectExplorer.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Controllers.Commands
{
    public class ChangeLevelCommand : ICommand
    {
        private static int activeLevel = 0;
        private static IList<ILevel> levels;
        private static LevelManager manager;

        private int amount;
        public ChangeLevelCommand(int amount, LevelManager manager)
        {
            this.amount = amount;
            ChangeLevelCommand.manager = manager;
            if (levels == null)
            {
                levels = manager.GetLevelList();
                for (int i = 0; i < levels.Count; i++)
                {
                    if (levels[i].LevelId == "start")
                    {
                        activeLevel = i;
                    }
                }
            }
        }

        public void Execute()
        {
            activeLevel += amount;
            if (activeLevel < 0)
                activeLevel += levels.Count;
            activeLevel %= levels.Count;
            manager.ChangeLevel(levels[activeLevel].LevelId);
        }
    }
}
