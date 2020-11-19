using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        private void DirectoryExpanded(object sender, DirectoryExpandedEventArgs e)
        {
            var dirs = e.currentPath.Split("\\");
            string parentDir = null;
            if (dirs.Length > 2) parentDir = dirs[dirs.Length - 3];



            var tn = treeView1.Nodes;
            //var tn = treenodes;
            string show = "";
            show += "Parent node if: " + parentDir + "\n";
            //show += "Selected: " + e.currentPath + "\n";
            //show += "Current name: " + e.currentName + "\n";
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



            MessageBox.Show(show);

            parentNode?.Nodes?.Add(e.currentName)?.Nodes.Add("_");

            /*
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


            
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void treeView1_AfterSelect(object sender, EventArgs e)
        {
           
        }

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
            try
            {
                model.ListDir(e.Node.Text);
                //DirectoryExpanded(this, new DirectoryExpandedEventArgs(e.Node.Parent?.Name, e.Node.FullPath, e.Node.Name));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Access denied", "Access denied",MessageBoxButtons.OK);
                e.Node.Nodes.Add("_"); //TODO: get it out
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
