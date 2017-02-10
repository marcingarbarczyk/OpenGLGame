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
    public partial class End : Form
    {
        public int points { get; set; }
        public string endText { get; set; }
        public bool victory { get; set; }
        private string endbg = "assets/graphics/gameoverbg.jpg";
        private string victorybg = "assets/graphics/gameoverbg.jpg";
        private string endbutton = "assets/graphics/buttons/end.png";
        private string endhoverbutton = "assets/graphics/buttons/end_hover.png";
        private string startbutton = "assets/graphics/buttons/new.png";
        private string starthoverbutton = "assets/graphics/buttons/new_hover.png";
        private WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

        public End()
        {
            InitializeComponent();
        }

        private void End_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void End_Load(object sender, EventArgs e)
        {
            lblPoints.Text = this.points.ToString();

            if (victory)
            {
                endgameBg.Image = Image.FromFile(victorybg);
            }
            else
            {
                endgameBg.Image = Image.FromFile(endbg);
            }

            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.Stop();
            }

            string intro = "assets/sounds/end.wav";

            if (System.IO.File.Exists(intro))
            {
                
                player.URL = intro;
                player.controls.play();
            }

            imgbtnStart.Image = Image.FromFile(startbutton);
            imgbtnEnd.Image = Image.FromFile(endbutton);
        }

        private void imgbtnStart_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            this.Hide();
            Menu menu = new Example1.Menu();
            menu.Show();
        }

        private void imgbtnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void imgbtnStart_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(starthoverbutton);
        }

        private void imgbtnStart_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(startbutton);
        }

        private void imgbtnEnd_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(endhoverbutton);
        }

        private void imgbtnEnd_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(endbutton);
        }
    }
}
