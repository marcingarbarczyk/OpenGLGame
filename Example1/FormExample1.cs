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
        bool left, right, space = false;
        List<GameObjects.Element> colliders = new List<GameObjects.Element>();

        GameObjects.Game game = new GameObjects.Game();
        GameObjects.Camera camera = new GameObjects.Camera();
        GameObjects.Player player = new GameObjects.Player();

        private void BasicGameSettings()
        {
            // Main game settings
            game.ctrl = this.openGLControl1;

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
            player.sizeX = 1;
            player.sizeY = 1;
            player.sizeZ = 1;
            player.gravity = -500;
            player.jumpHeight = 2;
            player.speedX = 0.1;
            player.isJumping = false;

            // Element settings
            colliders.Add(new GameObjects.Element
            {
                isCollider = true,
                coords = new double[,] {
                    {0, 0, 0 },
                    {1, 0, 0 },
                    {1, 0, 1 },
                    {0, 0, 1 }
                }
            });

            colliders.Add(new GameObjects.Element
            {
                isCollider = true,
                coords = new double[,] {
                    {2, 0, 0 },
                    {4, 0, 0 },
                    {4, 0, 1 },
                    {2, 0, 1 }
                }
            });

            colliders.Add(new GameObjects.Element
            {
                isCollider = true,
                coords = new double[,] {
                    {6, 1, 0 },
                    {4, 1, 0 },
                    {4, 1, 1 },
                    {6, 1, 1 }
                }
            });
        }

        #endregion

        private void MovePlayer(OpenGL gl)
        {
            if (left)
            {
                camera.centerX -= player.speedX;
                camera.eyeX -= player.speedX;
                player.moveX -= player.speedX;

            }
            if (right)
            {
                camera.centerX += player.speedX;
                camera.eyeX += player.speedX;
                player.moveX += player.speedX;
            }
            if (player.isJumping)
            {
                if (player.moveY >= player.jumpPoint)
                {
                    player.isJumping = false;
                    return;
                }
                player.moveY += 0.5;
            }
            if (!player.colliderY && !player.isJumping)
            {
                player.moveY -= 0.1;
            }
        }

        private void CheckCollider(OpenGL gl, GameObjects.Player player, List<GameObjects.Element> colliders)
        {
            for (int i = 0; i < colliders.Count; i++) {
                if (colliders[i].isCollider)
                {
                    double maxX = colliders[i].coords[0, 0], minX = colliders[i].coords[0, 0];
                    for (int j = 0; j <= colliders[i].coords.GetLength(1); j++)
                    {
                        if (maxX < colliders[i].coords[j, 0])
                            maxX = colliders[i].coords[j, 0];
                        if (minX > colliders[i].coords[j, 0])
                            minX = colliders[i].coords[j, 0];
                    }

                    if (player.moveX >= minX && player.moveX <= maxX && (float)player.moveY <= (float)colliders[i].coords[0, 1])
                    {
                        player.colliderY = true;
                        label3.Text = "Kolizja - " + i;
                        break;
                    }
                    player.colliderY = false;
                    
                }
            }
        }


        private void Draw(OpenGL gl, List<GameObjects.Element> colliders)
        {
            gl.Color(1.0, 1.0, 1.0);
            for (int j = 0; j < colliders.Count; j++)
            {
                gl.Begin(OpenGL.QUADS);
                for (int i = 0; i <= colliders[j].coords.GetLength(1); i++)
                {
                    gl.Vertex(colliders[j].coords[i, 0], colliders[j].coords[i, 1], colliders[j].coords[i, 2]);
                }
                gl.End();
            }
        }

        private void DrawHelpfulLines(OpenGL gl)
        {
            gl.Begin(OpenGL.LINES);
            gl.Color(1.0, 0, 0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 500);
            gl.Vertex(0, 0, 0);
            gl.Vertex(500, 0, 0);
            gl.Vertex(0, -500, 0);
            gl.Vertex(0, 500, 0);
            gl.End();
        }

        private void PlayerDraw(OpenGL gl)
        {
            gl.Translate(player.moveX, player.moveY, player.moveZ);
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
                game.LoadModel();
                init = false;
            }

            player.zX = player.moveX;
            player.zY = player.moveY;
            player.zZ = player.moveZ;


            CheckCollider(gl, player, colliders);
            MovePlayer(gl);
            

            

            gl.LookAt(camera.eyeX, camera.eyeY, camera.eyeZ, camera.centerX, camera.centerY, camera.centerZ, camera.upX, camera.upY, camera.upZ);
            DrawHelpfulLines(gl);
            game.DisplayModel(gl);
            gl.PushMatrix();
            PlayerDraw(gl);
            gl.PopMatrix();
            Draw(gl, colliders);
            label1.Text = player.moveX + " ";
        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        #region Keyboard evenets
        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
                left = true;

            if (e.KeyCode == Keys.D)
                right = true;

            if (e.KeyCode == Keys.Space)
            {
                player.isJumping = true;
                player.jumpPoint = player.moveY + player.jumpHeight;
            }
                    
        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                left = false;

            if (e.KeyCode == Keys.D)
                right = false;

            //if (e.KeyCode == Keys.Space)
                //space = false;

        }
        #endregion
    }
}