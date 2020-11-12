using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    public class FilesListedEventArgs : EventArgs
    {
        public List<MyFile> Files { get; set; }
        public FilesListedEventArgs(List<MyFile> fileList)
        {
            Files = fileList;
        }
    }
}
