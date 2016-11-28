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

        double x = -4, y = 4, z = -10;

        GameObjects.Camera camera = new GameObjects.Camera();

        private void PodstawoweUstawieniaGry()
        {
            camera.eyeX = 2;
            camera.eyeY = 3;
            camera.eyeZ = 5;

            camera.centerX = 0;
            camera.centerY = 0;
            camera.centerZ = 0;

            camera.upX = 0;
            camera.upY = 1;
            camera.upZ = 0;
        }


        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            // Podstawowe ustawienia gry i OpenGL
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Enable(OpenGL.DEPTH_TEST);

            PodstawoweUstawieniaGry();


            gl.LookAt(camera.eyeX, camera.eyeY, camera.eyeZ, camera.centerX, camera.centerY, camera.centerZ, camera.upX, camera.upY, camera.upZ);

            gl.Begin(OpenGL.LINES);
            gl.Color(1.0, 0, 0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 200);
            gl.Vertex(0, 0, 0);
            gl.Vertex(200, 0, 0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 200, 0);
            gl.End();

			gl.Begin(OpenGL.QUADS);
			gl.Color(1.0, 1.0, 0.0);
			gl.Vertex(0.0f, 0, 0.0f);
			gl.Vertex(5.0f, 0, 0f);
			gl.Vertex(5.0f, 0, 5.0f);
            gl.Vertex(0.0f, 0, 5.0f);
            gl.End();
        }


      

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dopecode.co.uk/buymeabeer");
        }

		private void openGLControl1_Load(object sender, EventArgs e)
		{

		}

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
            {
                x -= 0.1;
            }

            if (e.KeyCode == Keys.D)
            {
                x += 0.1;
            }

            if (e.KeyCode == Keys.W)
            {
                z += 0.1;
            }

            if (e.KeyCode == Keys.S)
            {
                z -= 0.1;
            }
        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}