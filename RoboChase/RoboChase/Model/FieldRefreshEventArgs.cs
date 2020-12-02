using System;
using RoboChase.Persistance;

namespace RoboChase.Model
{
    public class FieldRefreshEventArgs : EventArgs
    {
        public Position Position { get; private set; }
  
        public FieldRefreshEventArgs(Position p)
        {
            Position = p;
        }
    }
}