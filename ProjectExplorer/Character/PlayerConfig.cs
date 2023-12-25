using ProjectExplorer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character
{
    public static class PlayerConfig
    {
        public static float AttackDuration { get; set; } = 0.2f;
        public static float MovementSpeed { get; set; } = Tiling.ToPixels(5);
        public static int StartingHealth { get; set; } = 6;
        public static bool Invincible { get; set; } = false; 
        public static bool Sweep {  get; set; } = true;
    }
}
