using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SharpGL;

namespace Example1
{
    public partial class FormExample1 : Form
    {
        public FormExample1()
        {
            InitializeComponent();
        }

        #region Init game

        bool init = true;

        GameObjects.Camera camera = new GameObjects.Camera();
        GameObjects.Player player = new GameObjects.Player();

        private void BasicGameSettings()
        {
            // Camera settings
            camera.eyeX = 2;
            camera.eyeY = 3;
            camera.eyeZ = 8;
            camera.centerX = 0;
            camera.centerY = 0;
            camera.centerZ = 0;
            camera.upX = 0;
            camera.upY = 1;
            camera.upZ = 0;

            // Player settings
            player.pozX = 0;
            player.pozY = 0;
            player.pozZ = 0;
            player.sizeX = 1;
            player.sizeY = 1;
            player.sizeZ = 1;
        }

        #endregion

        private void PlayerDraw(OpenGL gl)
        {
            gl.Translate(player.pozX, 0, player.pozZ);
            gl.Begin(OpenGL.QUADS);
            gl.Color(1.0, 1.0, 0.0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(player.sizeX, 0, 0);
            gl.Vertex(player.sizeX, 0, player.sizeZ);
            gl.Vertex(0, 0, player.sizeZ);

            gl.Vertex(0, player.sizeY, 0);
            gl.Vertex(player.sizeX, player.sizeY, 0);
            gl.Vertex(player.sizeX, player.sizeY, player.sizeZ);
            gl.Vertex(0, player.sizeY, player.sizeZ);

            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, player.sizeZ);
            gl.Vertex(0, 1, player.sizeZ);
            gl.Vertex(0, player.sizeY, 0);

            gl.Vertex(player.sizeX, 0, 0);
            gl.Vertex(player.sizeX, 0, player.sizeZ);
            gl.Vertex(player.sizeX, player.sizeY, player.sizeZ);
            gl.Vertex(player.sizeX, player.sizeY, 0);

            gl.Vertex(0, 0, player.sizeZ);
            gl.Vertex(player.sizeX, 0, player.sizeZ);
            gl.Vertex(player.sizeX, 1, player.sizeZ);
            gl.Vertex(0, 1, player.sizeZ);


            gl.End();
        }


        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Enable(OpenGL.DEPTH_TEST);

            if(init)
            {
                BasicGameSettings();
                init = false;
            }
            


            gl.LookAt(camera.eyeX, camera.eyeY, camera.eyeZ, camera.centerX, camera.centerY, camera.centerZ, camera.upX, camera.upY, camera.upZ);

            gl.Begin(OpenGL.LINES);
            gl.Color(1.0, 0, 0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 500);
            gl.Vertex(0, 0, 0);
            gl.Vertex(500, 0, 0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0,500, 0);
            gl.End();

            PlayerDraw(gl);

            gl.Translate(0, 0, 0);
            gl.Begin(OpenGL.QUADS);
            gl.Color(0.0, 0.0, 1.0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(5, 0, 0);
            gl.Vertex(5, 0, 5);
            gl.Vertex(0, 0, 5);

            gl.End();


        }


      

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dopecode.co.uk/buymeabeer");
        }

		private void openGLControl1_Load(object sender, EventArgs e)
		{

		}


        #region Keyboard evenets
        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
            {
                player.pozX -= 0.1;
                camera.centerX -= 0.1;
                camera.eyeX -= 0.1;
            }

            if (e.KeyCode == Keys.D)
            {
                player.pozX += 0.1;
                camera.centerX += 0.1;
                camera.eyeX += 0.1;
            }

            if (e.KeyCode == Keys.W)
            {

            }

            if (e.KeyCode == Keys.S)
            {

            }
        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {

        }
        #endregion
    }
}