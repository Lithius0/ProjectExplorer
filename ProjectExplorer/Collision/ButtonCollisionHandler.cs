using ProjectExplorer.CharacterNS;
using ProjectExplorer.Tiles.ControlTiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    public class ButtonCollisionHandler : ICollisionHandler
    {
        private Button button;

        public ButtonCollisionHandler(Button button)
        {
            this.button = button;
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            // Maybe add some way to check for player only?
            if (other is IExclusion)
            {
                button.Press();
            }
        }
    }
}
