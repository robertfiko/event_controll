using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageDownloader.Model;

namespace ImageDownloader
{
    public partial class MainForm : Form
    {

        WebPage _model;
        public MainForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            numberOfPics.Text = "0";
            sexyBar.Value = 0;
            sexyBar.Visible = true;
            button1.Enabled = false;
            flowLayoutPanel1.Controls.Clear();
            _model = new WebPage(new Uri(textBox1.Text));
            _model.ImageLoaded  += ImageLoadedHandler;
            _model.LoadProgress += LoadProgressHandler;

            await _model.LoadImagesAsync();
            sexyBar.Visible = false;
            button1.Enabled = true;
        }

        private void ImageLoadedHandler(object obj, ImageLoadedEventArgs e)
        {
            PictureBox pic = new PictureBox();
            pic.Size = new Size(100, 100);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = e.image.image;
            flowLayoutPanel1.Controls.Add(pic);
            numberOfPics.Text = (Int32.Parse(numberOfPics.Text) + 1).ToString();
        }

        private void LoadProgressHandler(object obj, LoadProgressEventArgs e)
        {
            sexyBar.Value = e.progress;
        }
    }

    
}
