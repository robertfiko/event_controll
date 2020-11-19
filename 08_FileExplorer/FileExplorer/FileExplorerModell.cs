using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FileExplorer
{
    public class FileExplorerModell
    {
        public Dictionary<String, System.IO.DirectoryInfo> data { get; private set; }

        public FileExplorerModell() {
            data = new Dictionary<String, System.IO.DirectoryInfo>();
        }
        
        public void ListDrives()
        {
            var drives = System.IO.DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                data.Add(drive.RootDirectory.FullName, drive.RootDirectory);
            }
            OnDirectoryExpanded("/", "/", "/");
        }

        public void ListDir(String dirname)
        {
            if (data.ContainsKey(dirname)) 
            {
                foreach (var item in data[dirname].GetDirectories())
                {
                    if (item.Exists)
                    {
                        data.Add(item.Name, item);
                        OnDirectoryExpanded(dirname, item.FullName, item.Name);
                    }

                }
            }
            else
            {
                string cc = "";
                foreach (var item in data.Keys)
                {
                    cc += item.ToString();
                }
            }
            
        }



        public event EventHandler<DirectoryExpandedEventArgs> DirectoryExpanded; 

        private void OnDirectoryExpanded(string expandedDir, string subDirPath, string subDirName)
        {
            this.DirectoryExpanded?.Invoke(this, new DirectoryExpandedEventArgs(expandedDir, subDirPath, subDirName));
        }


}
}
