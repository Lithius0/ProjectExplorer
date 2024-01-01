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
        Up = 0,
        Down = 1, 
        Left = 2, 
        Right = 3,
    }

    public static class DirectionMethods
    {
        public static Direction Parse(string text)
        {
            text = text.ToLower();
            switch(text)
            {
                case "up":
                    return Direction.Up;
                case "down":
                    return Direction.Down;
                case "left":
                    return Direction.Left;
                case "right":
                    return Direction.Right;
                default:
                    throw new ArgumentException($"{text} is not a valid direction!");
            }
        }

        public static Vector2 ToVector2(this Direction d)
        {
            return d switch
            {
                Direction.Up => -Vector2.UnitY,
                Direction.Down => Vector2.UnitY,
                Direction.Left => -Vector2.UnitX,
                Direction.Right => Vector2.UnitX,
                _ => throw new NotImplementedException(),
            };
        }

        public static Direction Flip(this Direction d)
        {
            return d switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new NotImplementedException(),
            };
        }

        public static Direction ParseVector(Vector2 v)
        {
            if(v.Equals(Vector2.UnitY))
                return Direction.Down;

            if(v.Equals(Vector2.UnitX))
                return Direction.Right;

            if(v.Equals(-Vector2.UnitY))
                return Direction.Up;

            if(v.Equals(-Vector2.UnitX))
                return Direction.Left;

            throw new NotImplementedException();
        }
    }
}
