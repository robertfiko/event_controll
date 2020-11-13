using System;

namespace CrazyBot
{
    public class FieldRefreshEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public FieldRefreshEventArgs(int xx, int yy)
        {
            X = xx;
            Y = yy;
        }
    }
}