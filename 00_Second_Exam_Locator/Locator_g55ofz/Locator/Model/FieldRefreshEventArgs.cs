using System;

namespace Locator.Model
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