using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Controllers.Commands;

namespace ProjectExplorer.Controllers
{
    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> pressedMappings;
        private Dictionary<Keys, ICommand> heldMappings;
        private Dictionary<Keys, ICommand> releasedMappings;

        private Keys[] previousKeys;

        public KeyboardController()
        {
            heldMappings = new Dictionary<Keys, ICommand>();
            pressedMappings = new Dictionary<Keys, ICommand>();
            releasedMappings = new Dictionary<Keys, ICommand>();

            previousKeys = new Keys[0];
        }

        public void RegisterPressCommand(Keys key, ICommand command)
        {
            pressedMappings[key] = command;
        }

        public void RegisterHeldCommand(Keys key, ICommand command)
        {
            heldMappings[key] = command;
        }
        public void RegisterReleasedCommand(Keys key, ICommand command)
        {
            releasedMappings[key] = command;
        }

        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (heldMappings.ContainsKey(key))
                {
                    heldMappings[key].Execute();
                }
                if (!previousKeys.Contains(key) && pressedMappings.ContainsKey(key))
                {
                    pressedMappings[key].Execute();
                }
            }
            foreach (Keys key in previousKeys)
            {
                if (!pressedKeys.Contains(key) && releasedMappings.ContainsKey(key))
                {
                    releasedMappings[key].Execute();
                }
            }

            previousKeys = pressedKeys;
        }
    }
}
