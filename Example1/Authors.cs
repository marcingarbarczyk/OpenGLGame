using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Example1
{
    public partial class Authors : Form
    {
        public Authors()
        {
            InitializeComponent();
        }

        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        private string authorsbg = "assets/graphics/authorsbg.jpg";
        private string backbutton = "assets/graphics/buttons/back.png";
        private string backhoverbutton = "assets/graphics/buttons/back_hover.png";

        private void Authors_Load(object sender, EventArgs e)
        {

            string intro = "assets/sounds/authors.wav";

            if (System.IO.File.Exists(intro))
            {
                player.URL = intro;
                player.controls.play();
            }

            authorsBg.Image = Image.FromFile(authorsbg);
            btnimgBack.Image = Image.FromFile(backbutton);
        }

        private void Authors_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnimgBack_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void btnimgBack_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(backhoverbutton);
        }

        private void btnimgBack_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(backbutton);
        }
    }
}
