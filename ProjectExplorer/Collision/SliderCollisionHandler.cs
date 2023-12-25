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
    public class SliderCollisionHandler : ICollisionHandler
    {
        private Slider slider;

        public SliderCollisionHandler(Slider slider)
        {
            this.slider = slider;
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {

            if (other is IExclusion exclusion)
            {
                // Prevent player from hitting the slider from below or above. They can only push sideways.
                if (intersection.Width < intersection.Height)
                {
                    float offset = intersection.Width;
                    if (intersection.Right == slider.GetCollider().Right)
                    {
                        // Player has pushed from the right, so the offset needs to be negative.
                        offset = -offset;
                    }
                    slider.SetPosition(slider.Position.X + offset);
                }

                // Primarily to give the player some resistance to the slider.
                // And to prevent them from walking through it when it's at a limit.
                exclusion.Exclude(intersection);
            }
        }
    }
}
