using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CrazyBot
{
    public class GridButton : Button
    {
        
        public GridButton(int xx, int yy)
        {
            X = xx;
            Y = yy;
        }

        public Position getPosition()
        {
            return new Position(X, Y);
        }
        
        public int Y { get; private set; }

        public int X { get; private set; }
    }
}
