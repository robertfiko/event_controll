using System;

namespace CrazyBot
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        

        public override bool Equals(Object other)
        {
            if ((other == null) || !this.GetType().Equals(other.GetType()))
            {
                return false;
            }
            else
            {
                Position p = (Position)other;
                return (X == p.X) && (Y == p.Y);
            }
        }

        
    }
}
