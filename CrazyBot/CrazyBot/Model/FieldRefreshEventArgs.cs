using System;
using CrazyBot.Persistance;

namespace CrazyBot.Model
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