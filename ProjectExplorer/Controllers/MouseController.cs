using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Controllers.Commands;

namespace ProjectExplorer.Controllers
{
    public class MouseController : IController
    {
        public enum MouseButton { Left, Right }

        private Dictionary<MouseButton, ICommand> pressedMappings;
        private Dictionary<MouseButton, ICommand> heldMappings;
        private Dictionary<MouseButton, ICommand> releasedMappings;

        private MouseState previousMouseState;

        public MouseController()
        {
            heldMappings = new Dictionary<MouseButton, ICommand>();
            pressedMappings = new Dictionary<MouseButton, ICommand>();
            releasedMappings = new Dictionary<MouseButton, ICommand>();

            previousMouseState = Mouse.GetState();
        }

        public void RegisterPressCommand(MouseButton button, ICommand command)
        {
            pressedMappings[button] = command;
        }

        public void RegisterHeldCommand(MouseButton button, ICommand command)
        {
            heldMappings[button] = command;
        }
        public void RegisterReleasedCommand(MouseButton button, ICommand command)
        {
            releasedMappings[button] = command;
        }

        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();
            // Only update if mouse click happened within the game window.
            if (Coordinator.Instance.ScreenManager.GameWindow.Contains(currentMouseState.Position))
            {
                ExecuteCommandsFromButtonStates(previousMouseState.LeftButton, currentMouseState.LeftButton, MouseButton.Left);
                ExecuteCommandsFromButtonStates(previousMouseState.RightButton, currentMouseState.RightButton, MouseButton.Right);

                previousMouseState = currentMouseState;
            }
        }

        private void ExecuteCommandsFromButtonStates(ButtonState previousState, ButtonState currentState, MouseButton button)
        {
            if ((currentState == ButtonState.Pressed) && heldMappings.ContainsKey(button))
            {
                heldMappings[button].Execute();
            }
            
            if (WasPressed(previousState, currentState) && (pressedMappings.ContainsKey(button)))
            {
                pressedMappings[button].Execute();
            }
            
            if(WasReleased(previousState, currentState) && (releasedMappings.ContainsKey(button)))
            {
                releasedMappings[button].Execute();
            }
        }

        private bool WasPressed(ButtonState previousState, ButtonState currentState)
        {
            return (currentState == ButtonState.Pressed) && (previousState == ButtonState.Released);
        }

        private bool WasReleased(ButtonState previousState, ButtonState currentState)
        {
            return (currentState == ButtonState.Released) && (previousState == ButtonState.Pressed);
        }
    }
}
