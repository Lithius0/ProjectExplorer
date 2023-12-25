using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ProjectExplorer.Character;
using System.Diagnostics;
using ProjectExplorer.UI;

namespace ProjectExplorer.DebugMode
{
    public class DebugInfo : Screen
    {
        public DebugInfo(IPlayer player) : base(Vector2.Zero, 1000)
        {
            AddModule(new InventoryInfo(player.Inventory, Vector2.Zero));
            AddModule(new PlayerInfo(player, new Vector2(100, 0)));
        }
    }
}
