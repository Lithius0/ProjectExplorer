using ProjectExplorer.UI;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.UI
{
    public class MainHud : Screen
    {
        public MainHud(Vector2 position, int layer = 0) : base(position, layer)
        {
            AddModule(new HealthCount(Tiling.ToPixels(20, 1).ToVector2()));
            AddModule(new CoinCount(Tiling.ToPixels(15, 1).ToVector2()));
            AddModule(new PrimaryEquippedItem(Tiling.ToPixels(17, 0).ToVector2()));
            AddModule(new SecondaryEquippedItem(Tiling.ToPixels(15, 0).ToVector2()));
        }
    }
}
