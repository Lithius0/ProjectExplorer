using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    public class TestButton : Button
    {
        public TestButton(Vector2 position) : base(position)
        {
            PressEnd += (_, _) =>
            {
                Debug.WriteLine("end");
            };
            PressStart += (_, _) =>
            {
                Debug.WriteLine("start");
            };
        }
    }
}
