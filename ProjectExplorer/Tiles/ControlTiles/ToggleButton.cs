using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    public class ToggleButton : Button
    {
        private bool previousState = false;
        public EventHandler Toggled;

        public ToggleButton(Vector2 position) : base(position) { }

        public override void Update(GameTime gameTime)
        {
            if (!previousState && pressed)
            {
                Toggle();
            }

            previousState = pressed;
            // Collision handler will call press every frame that the player is colliding with button.
            pressed = false;
        }

        public void Toggle()
        {
            on = !on;
            if (on)
                PressStart?.Invoke(this, EventArgs.Empty);
            else
                PressEnd?.Invoke(this, EventArgs.Empty);

            Toggled?.Invoke(this, EventArgs.Empty);
        }
    }
}
