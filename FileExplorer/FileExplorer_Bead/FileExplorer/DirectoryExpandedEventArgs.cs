using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    public class DirectoryExpandedEventArgs : EventArgs
    {
        public String parent { get; private set; }
        public String currentPath { get; private set; }
        public String currentName { get; private set; }

        public DirectoryExpandedEventArgs(string p, string cp, string cn)
        {
            parent = p;
            currentPath = cp;
            currentName = cn;

        }



        


    }
}
