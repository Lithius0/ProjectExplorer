using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    public enum CollisionGroup
    {
        Players,
        Enemies,
        Items,
        Tiles,
        Projectiles,
        Triggers,
    }

    public static class CollisionGroupMethods
    { 
        public static Color GetDebugColor(this CollisionGroup c)
        {
            return c switch
            {
                CollisionGroup.Players => Color.Green,
                CollisionGroup.Enemies => Color.Red,
                CollisionGroup.Items => Color.Blue,
                CollisionGroup.Projectiles => Color.Yellow,
                CollisionGroup.Triggers => Color.Purple,
                _ => Color.White,
            };
        }
    }
}
