using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer
{
    public enum Direction
    {
        UP = 0,
        DOWN = 1, 
        LEFT = 2, 
        RIGHT = 3,
    }

    public static class DirectionMethods
    {
        public static Direction Parse(string text)
        {
            text = text.ToLower();
            switch(text)
            {
                case "up":
                    return Direction.UP;
                case "down":
                    return Direction.DOWN;
                case "left":
                    return Direction.LEFT;
                case "right":
                    return Direction.RIGHT;
                default:
                    throw new ArgumentException($"{text} is not a valid direction!");
            }
        }

        public static Vector2 GetVector2(this Direction d)
        {
            return d switch
            {
                Direction.UP => -Vector2.UnitY,
                Direction.DOWN => Vector2.UnitY,
                Direction.LEFT => -Vector2.UnitX,
                Direction.RIGHT => Vector2.UnitX,
                _ => throw new NotImplementedException(),
            };
        }

        public static Direction Flip(this Direction d)
        {
            return d switch
            {
                Direction.UP => Direction.DOWN,
                Direction.DOWN => Direction.UP,
                Direction.LEFT => Direction.RIGHT,
                Direction.RIGHT => Direction.LEFT,
                _ => throw new NotImplementedException(),
            };
        }

        public static Direction GetDirection(Vector2 v)
        {
            if(v.Equals(Vector2.UnitY))
                return Direction.DOWN;

            if(v.Equals(Vector2.UnitX))
                return Direction.RIGHT;

            if(v.Equals(-Vector2.UnitY))
                return Direction.UP;

            if(v.Equals(-Vector2.UnitX))
                return Direction.LEFT;

            throw new NotImplementedException();
        }
    }
}
