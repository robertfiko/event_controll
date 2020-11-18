using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    public class MyFile
    {
        public String Name { get; set; }
        public long Size { get; set; }
        public DateTime CreationTime { get; set; }

        public MyFile(string n, long s, DateTime ct)
        {
            Name = n;
            Size = s;
            CreationTime = ct;
        }
    }
}
