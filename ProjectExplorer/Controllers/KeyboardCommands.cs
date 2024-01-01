using ProjectExplorer.Items;
using ProjectExplorer.Character;
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
            controller.RegisterPressCommand(Keys.W, new MoveCommand(player, Direction.Up));
            controller.RegisterPressCommand(Keys.A, new MoveCommand(player, Direction.Left));
            controller.RegisterPressCommand(Keys.S, new MoveCommand(player, Direction.Down));
            controller.RegisterPressCommand(Keys.D, new MoveCommand(player, Direction.Right));
            controller.RegisterReleasedCommand(Keys.W, new StopCommand(player, Direction.Up));
            controller.RegisterReleasedCommand(Keys.A, new StopCommand(player, Direction.Left));
            controller.RegisterReleasedCommand(Keys.S, new StopCommand(player, Direction.Down));
            controller.RegisterReleasedCommand(Keys.D, new StopCommand(player, Direction.Right));
            controller.RegisterPressCommand(Keys.J, new AttackCommand(player));
            controller.RegisterPressCommand(Keys.K, new UseSecondaryCommand(player));
            controller.RegisterPressCommand(Keys.E, new DamageCommand(player));
            controller.RegisterPressCommand(Keys.Up, new SelectCommand(player, Direction.Up));
            controller.RegisterPressCommand(Keys.Left, new SelectCommand(player, Direction.Left));
            controller.RegisterPressCommand(Keys.Down, new SelectCommand(player, Direction.Down));
            controller.RegisterPressCommand(Keys.Right, new SelectCommand(player, Direction.Right));
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
