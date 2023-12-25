using ProjectExplorer.Items;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.UI;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework.Input;
using ProjectExplorer.Controllers.Commands;

namespace ProjectExplorer.Controllers
{
    public static class KeyboardCommands
    {
        public static void RegisterPlayerCommands(KeyboardController controller, IPlayer player)
        {
            controller.RegisterPressCommand(Keys.W, new MoveCommand(player, Direction.UP));
            controller.RegisterPressCommand(Keys.A, new MoveCommand(player, Direction.LEFT));
            controller.RegisterPressCommand(Keys.S, new MoveCommand(player, Direction.DOWN));
            controller.RegisterPressCommand(Keys.D, new MoveCommand(player, Direction.RIGHT));
            controller.RegisterReleasedCommand(Keys.W, new StopCommand(player, Direction.UP));
            controller.RegisterReleasedCommand(Keys.A, new StopCommand(player, Direction.LEFT));
            controller.RegisterReleasedCommand(Keys.S, new StopCommand(player, Direction.DOWN));
            controller.RegisterReleasedCommand(Keys.D, new StopCommand(player, Direction.RIGHT));
            controller.RegisterPressCommand(Keys.J, new AttackCommand(player));
            controller.RegisterPressCommand(Keys.K, new UseSecondaryCommand(player));
            controller.RegisterPressCommand(Keys.E, new DamageCommand(player));
            controller.RegisterPressCommand(Keys.Up, new SelectCommand(player, Direction.UP));
            controller.RegisterPressCommand(Keys.Left, new SelectCommand(player, Direction.LEFT));
            controller.RegisterPressCommand(Keys.Down, new SelectCommand(player, Direction.DOWN));
            controller.RegisterPressCommand(Keys.Right, new SelectCommand(player, Direction.RIGHT));
        }

        public static void RegisterGameCommands(KeyboardController controller, Game1 game, ScreenManager screen)
        {
            controller.RegisterPressCommand(Keys.Q, new CommandQuit(game));
            controller.RegisterPressCommand(Keys.R, new ResetCommand());
            controller.RegisterPressCommand(Keys.OemTilde, new DebugCommand(screen));
            controller.RegisterPressCommand(Keys.OemMinus, new ZoomCommand(game.Screen, -1));
            controller.RegisterPressCommand(Keys.OemPlus, new ZoomCommand(game.Screen, 1));
            controller.RegisterPressCommand(Keys.P, new PauseCommand());
            controller.RegisterPressCommand(Keys.OemCloseBrackets, new NextCommand());
            controller.RegisterPressCommand(Keys.Tab, new InventoryCommand());
        }
    }
}
