using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace FileExplorer
{
    public partial class FileExplorer : Form
    {
        private FileExplorerModell model { get; set; }
        public FileExplorer()
        {
            InitializeComponent();
            model = new FileExplorerModell();
            model.ListDrives();
            model.DirectoryExpanded += DirectoryExpanded;
            model.FilesListed += onFileListed;
            listView1.DoubleClick += openFile;

        }

        private void DirectoryExpanded(object sender, DirectoryExpandedEventArgs e)
        {
            /* 
             * NOT WORKING !!!!!
             * 
             * var tn = treeView1.Nodes;
             //var tn = treenodes;
             string show = "";
             TreeNode parentNode = null;

             foreach (TreeNode node in tn)
             {
                 show += node.Text + " - a \n";
                 if (e.parent == node.Text)
                 {
                     show += "found!!!! \n";
                     parentNode = node;
                     //break;
                 }
             }


             show += parentNode?.Text + " - Parent node nodes: " + parentNode?.Nodes?.Count;

             parentNode?.Nodes?.Add(e.currentName)?.Nodes.Add("_");


             var tmp = model.data[e.currentName];
             var dir = tmp.EnumerateDirectories();
             if (dir.ToArray().Length == 0)
             {
                 parentNode?.Nodes.Add("_");
             }
             foreach (var item in dir)
             {
                 parentNode?.Nodes.Add(item.ToString());
             }*/

            model.clicked.Nodes.Add(e.currentName).Nodes.Add("_");

        }


        //nope
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        //nope
        private void treeView1_AfterSelect(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dirinfo = null;
            foreach (var item in model.data)
            {
                if (item.Key == (e as TreeViewEventArgs).Node.Text) dirinfo = item.Value;
            }
            
        }

        //yes
        private void ectedIndexChange(object sender, EventArgs e)
        {
            //Load
            foreach (var item in model.data.Values)
            {
                treeView1.Nodes.Add(item.ToString());
            }

            foreach (TreeNode node in treeView1.Nodes)
            {
                node.Nodes.Add("-");
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            model.clicked = e.Node;
            try
            {
                model.ListDir(e.Node.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Access denied", "Access denied",MessageBoxButtons.OK);
                e.Node.Nodes.Add("_"); //TODO: get it out
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            model.ListFiles(e.Node.Text);
        }

        private string formatSize(long byteSize)
        {
            double doubleByte = byteSize;
            string[] mertek = new string[4] { "B", "KB", "MB", "GB" };
            int counter = 0;
            while (doubleByte > 1024 && counter < 3)
            {
                doubleByte /= 1024.0;
                counter++;
            }
            return doubleByte.ToString() + " " + mertek[counter];
        }

        private void onFileListed(object sender, FilesListedEventArgs e)
        {
            listView1.Items.Clear();
            foreach (var d in e.Files)
            {
                String[] data = { d.Name, formatSize(d.Size), d.CreationTime.ToString() };
                ListViewItem lv = new ListViewItem(data);
                listView1.Items.Add(lv);
            }

            this.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void openFile(object sender, EventArgs e)
        {
            String path = model.opens[model.opens.Count - 1] + "\\" + listView1.SelectedItems[0].Text;            

            ProcessStartInfo proc = new ProcessStartInfo(path);
            proc.UseShellExecute = true;
            Process.Start(proc);
        }
    }
}
