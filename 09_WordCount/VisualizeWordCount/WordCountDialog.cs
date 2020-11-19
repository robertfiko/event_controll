using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordCount;

namespace VisualizeWordCount
{
    public partial class WordCountDialog : Form
    {
        public WordCountDialog()
        {
            InitializeComponent();

            statistics = new Statistics();
            openFileDialogMenuItem.Click += OpenDialog;
            countWordsFileDiaog.Click += CalculateStatistics;
        }

        private Statistics statistics;
        private void OpenDialog(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        statistics.Load(openFileDialog.FileName);
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show("File reading is unsuccessful!\n" + ex.Message,
                        "Error", MessageBoxButtons.OK);
                        return;
                    }
                    if (!String.IsNullOrEmpty(statistics.FileContent)
                    && statistics.FileContent == textBox.Text)
                        return;
                    listBoxCounter.Items.Clear();
                    textBox.Text = statistics.FileContent;
                }
            }



        }

        private void CalculateStatistics(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(statistics.FileContent))
            {
                MessageBox.Show("No text is loaded!\n" ,
                "Error", MessageBoxButtons.OK);
                return;
            }

            statistics.CountWords();
            var pairs = statistics.WordCount.OrderByDescending(e => e.Value);
            listBoxCounter.Items.Clear();
            listBoxCounter.BeginUpdate();
            foreach (var pair in pairs)
            {
                listBoxCounter.Items.Add(pair.Key + ": " + pair.Value);
            }
            listBoxCounter.EndUpdate();
        }

        private void WordCountDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
