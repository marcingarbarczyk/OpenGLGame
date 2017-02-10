using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Example1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

        private string mainmenubg = "assets/graphics/mainmenubg.jpg";
        private string startbutton = "assets/graphics/buttons/start.png";
        private string starthoverbutton = "assets/graphics/buttons/start_hover.png";
        private string authorbutton = "assets/graphics/buttons/authors.png";
        private string authorhoverbutton = "assets/graphics/buttons/authors_hover.png";
        private string exitbutton = "assets/graphics/buttons/exit.png";
        private string exithoverbutton = "assets/graphics/buttons/exit_hover.png";
        private string intro = "assets/sounds/begin.wav";

        private void Menu_Load(object sender, EventArgs e)
        {
            

            if (System.IO.File.Exists(intro))
            {
                
                player.URL = intro;
                player.controls.play();
            }

            mainMenuBg.Image = Image.FromFile(mainmenubg);
            imgbtnNewGame.Image = Image.FromFile(startbutton);
            imgbtnAuthors.Image = Image.FromFile(authorbutton);
            imgbtnExit.Image = Image.FromFile(exitbutton);
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void imgbtnNewGame_Click(object sender, EventArgs e)
        {
            GameWindow game = new Example1.GameWindow();
            game.Focus();
            game.Show();
            this.Hide();
            player.controls.stop();
        }

        private void imgbtnAuthors_Click(object sender, EventArgs e)
        {
            Authors authors = new Authors();
            authors.Show();
            this.Hide();
            player.controls.stop();
        }

        private void imgbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imgbtnNewGame_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(starthoverbutton);
        }

        private void imgbtnNewGame_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(startbutton);
        }

        private void imgbtnAuthors_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(authorhoverbutton);
        }

        private void imgbtnAuthors_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(authorbutton);
        }

        private void imgbtnExit_MouseHover(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(exithoverbutton);
        }

        private void imgbtnExit_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Image.FromFile(exitbutton);
        }

        private void mainMenuBg_Click(object sender, EventArgs e)
        {

        }
    }
}
