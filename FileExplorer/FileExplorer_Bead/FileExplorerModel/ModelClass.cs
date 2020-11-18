using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileExplorerModel
{
    public class ModelClass
    {
        public Dictionary<String, System.IO.DirectoryInfo> data { get; private set; }
        public void ListDrives() {
            var drives = System.IO.DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                data.Add(drive.RootDirectory.FullName, drive.RootDirectory);
            }
           
        }

        
    }
}
