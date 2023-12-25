using ProjectExplorer.Items;
using ProjectExplorer.Levels;
using ProjectExplorer.Character;
using Microsoft.Xna.Framework.Input;
using ProjectExplorer.Controllers.Commands;

namespace ProjectExplorer.Controllers
{
    public static class MouseCommands
    {
        public static void RegisterGameCommands(MouseController controller, Game1 game, LevelManager levelManager)
        {
            controller.RegisterPressCommand(MouseController.MouseButton.Right, new ChangeLevelCommand(1, levelManager));
            controller.RegisterPressCommand(MouseController.MouseButton.Left, new ChangeLevelCommand(-1, levelManager));
        }
    }
}
