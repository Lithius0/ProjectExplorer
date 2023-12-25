using Microsoft.Xna.Framework;
using ProjectExplorer.SpriteUtil.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    public class SliderDisplay : TextTile
    {
        public SliderDisplay(Vector2 position, Slider slider, int precision) : base("DuskB2", position, "")
        {
            string formatString = "%." + precision + "f";
            slider.Changed += (object sender, SliderValueArgs e) =>
            {
                SetText(string.Format(formatString, e.NewValue));
            };
            SetText(string.Format(formatString, slider.Value));
        }
    }
}
