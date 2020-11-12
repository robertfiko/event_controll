using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace FileExplorer
{
    public class FileExplorerModell
    {
        public TreeNode clicked;
        public List<System.IO.DirectoryInfo> opens { get; set; }
        public Dictionary<String, System.IO.DirectoryInfo> data { get; private set; }

        public FileExplorerModell() {
            data = new Dictionary<String, System.IO.DirectoryInfo>();
            opens = new List<System.IO.DirectoryInfo>();
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

        public void ListFiles(string file)
        {
            var dirinfo = data[file];
            opens.Add(dirinfo);

            System.IO.DirectoryInfo dir = opens[opens.Count - 1];

            List<MyFile> list = new List<MyFile>();
            IEnumerable<System.IO.FileInfo> dirs = null;
            try
            {
                dirs = dir?.EnumerateFiles();
            }

            catch (Exception e) {
                return; 
            }

            foreach (var item in dirs) //exceptiiion
            {
                MyFile mylovingfile = new MyFile(item.Name, item.Length, item.CreationTime);
                list.Add(mylovingfile);
            }
            
            OnFilesListed(list);
        }



        public event EventHandler<DirectoryExpandedEventArgs> DirectoryExpanded;
        public event EventHandler<FilesListedEventArgs> FilesListed;

        private void OnDirectoryExpanded(string expandedDir, string subDirPath, string subDirName)
        {
            this.DirectoryExpanded?.Invoke(this, new DirectoryExpandedEventArgs(expandedDir, subDirPath, subDirName));
        }

        private void OnFilesListed(List<MyFile> fileList)
        {
            this.FilesListed?.Invoke(this, new FilesListedEventArgs(fileList));
        }


}
}
