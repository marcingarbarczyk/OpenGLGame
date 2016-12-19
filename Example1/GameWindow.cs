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
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        #region Init game

        bool init = true;
        bool left, right, space = false;
        List<GameObjects.Block> blocks = new List<GameObjects.Block>();
        GameObjects.Model[] models = new GameObjects.Model[200];

        GameObjects.Game game = new GameObjects.Game();
        GameObjects.Camera camera = new GameObjects.Camera();
        GameObjects.Player player = new GameObjects.Player();

        private void BasicGameSettings()
        {
            // Main game settings
            game.ctrl = this.openGLControl1;
            game.epsilon = 0.01f;

            // Load models
            models[0] = game.LoadModel("assets/models/key.obj");

            // Camera settings
            camera.eyeX = 0;
            camera.eyeY = 2;
            camera.eyeZ = 15;
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
            player.x = 0.0f;
            player.y = 0.0f;
            player.z = 0;
            player.speed = 0.1f;
            player.jumpMax = 1.5f;
            player.weight = 0.5f;
            player.modelStandard = models[0];



            // Blocks
            blocks.Add(new GameObjects.Block
            {
                textureID = 1,
                collider = new GameObjects.Collider
                {
                    leftViewCoords = new float[,]
                    {
                        {2, 0, 0},
                        {2, 0, 2},
                        {2, 1, 2},
                        {2, 1, 0}
                    },
                    rightViewCoords = new float[,]
                    {
                        {4, 0, 0},
                        {4, 0, 2},
                        {4, 1, 2},
                        {4, 1, 0}
                    },
                    topViewCoords = new float[,]
                    {
                        {2, 1, 0},
                        {2, 1, 2},
                        {4, 1, 2},
                        {4, 1, 0}
                    },
                    bottomViewCoords = new float[,]
                    {
                        {2, 0, 0},
                        {2, 0, 2},
                        {4, 0, 2},
                        {4, 0, 0}
                    },
                    frontViewCoords = new float[,]
                    {
                        {2, 0, 2 },
                        {2, 1, 2 },
                        {4, 1, 2 },
                        {4, 0, 2 },
                    }
                    
                }

            });


            blocks.Add(new GameObjects.Block
            {
                textureID = 1,
                collider = new GameObjects.Collider
                {
                    leftViewCoords = new float[,]
                    {
                        {0, -2, 0},
                        {0, -2, 2},
                        {0, 0, 2},
                        {0, 0, 0}
                    },
                    rightViewCoords = new float[,]
                    {
                        {4, -2, 0},
                        {4, -2, 2},
                        {4, 0, 2},
                        {4, 0, 0}
                    },
                    topViewCoords = new float[,]
                    {
                        {0, 0, 0},
                        {0, 0, 2},
                        {4, 0, 2},
                        {4, 0, 0}
                    },
                    bottomViewCoords = new float[,]
                    {
                        {0, -2, 0},
                        {0, -2, 2},
                        {4, -2, 2},
                        {4, -2, 0}
                    },
                    frontViewCoords = new float[,]
                    {
                        {0, -2, 2},
                        {0, 0, 2},
                        {4, 0, 2},
                        {4, -2, 2}
                    }
                }

            });

            blocks.Add(new GameObjects.Block
            {
                textureID = 1,
                collider = new GameObjects.Collider
                {
                    leftViewCoords = new float[,]
                    {
                        {0, 3, 0},
                        {0, 3, 2},
                        {0, 5, 2},
                        {0, 5, 0}
                    },
                    rightViewCoords = new float[,]
                    {
                        {4, 3, 0},
                        {4, 3, 2},
                        {4, 5, 2},
                        {4, 5, 0}
                    },
                    topViewCoords = new float[,]
                    {
                        {0, 5, 0},
                        {0, 5, 2},
                        {4, 5, 2},
                        {4, 5, 0}
                    },
                    bottomViewCoords = new float[,]
                    {
                        {0, 3, 0},
                        {0, 3, 2},
                        {4, 3, 2},
                        {4, 3, 0}
                    },
                    frontViewCoords = new float[,]
                    {
                        {0, 3, 2},
                        {0, 5, 2},
                        {4, 5, 2},
                        {4, 3, 2}
                    }
                }

            });
        }

        #endregion

        private void MovePlayer(OpenGL gl)
        {
            if (left && !player.colliderXleft)
            {
                camera.centerX -= player.speed;
                camera.eyeX -= player.speed;
                player.x -= player.speed;
                player.back = true;
            }

            if (right && !player.colliderXright)
            {
                camera.centerX += player.speed;
                camera.eyeX += player.speed;
                player.x += player.speed;
                player.back = false;
            }

            if (player.isJumping)
            {
                if (player.colliderYtop)
                {
                    player.isJumping = false;

                }
                else
                {
                    if (player.y <= player.jumpingLimit)
                        player.y += 0.1f;
                    else
                        player.isJumping = false;
                }
            }

            if (!player.colliderYbottom && !player.isJumping)
            {
                player.y -= 0.1f;
            }
        }

        private void CheckCollider(OpenGL gl, GameObjects.Player player, List<GameObjects.Block> blocks)
        {
            double maxX, maxY, maxZ, minX, minY, minZ;
            int left, right, top, bottom;
            left = right = top = bottom = -1;


            for (int i = 0; i < blocks.Count; i++)
            {


                // If hit left wall
                if (left == -1 || left == i)
                {
                    maxY = blocks[i].collider.leftViewCoords[0, 1];
                    minY = blocks[i].collider.leftViewCoords[0, 1];

                    for (int j = 0; j <= blocks[i].collider.leftViewCoords.GetLength(1); j++)
                    {
                        if (maxY < blocks[i].collider.leftViewCoords[j, 1])
                            maxY = blocks[i].collider.leftViewCoords[j, 1];

                        if (minY > blocks[i].collider.leftViewCoords[j, 1])
                            minY = blocks[i].collider.leftViewCoords[j, 1];
                    }

                    if (Math.Abs((player.x + player.sizeX) - blocks[i].collider.leftViewCoords[0, 0]) <= game.epsilon && ((player.y <= maxY && player.y >= minY) || (player.y + player.sizeY <= maxY && player.y + player.sizeY >= minY)))
                    {
                        player.colliderXright = true;
                        left = i;
                        label3.Text = "Kolizja œciany lewej";
                    }
                    else
                    {
                        player.colliderXright = false;
                        left = -1;
                    }
                        

                }




                // If hit right wall
                if (right == -1 || right == i)
                {
                    maxY = blocks[i].collider.rightViewCoords[0, 1];
                    minY = blocks[i].collider.rightViewCoords[0, 1];

                    for (int j = 0; j <= blocks[i].collider.rightViewCoords.GetLength(1); j++)
                    {
                        if (maxY < blocks[i].collider.rightViewCoords[j, 1])
                            maxY = blocks[i].collider.rightViewCoords[j, 1];

                        if (minY > blocks[i].collider.rightViewCoords[j, 1])
                            minY = blocks[i].collider.rightViewCoords[j, 1];
                    }

                    if (Math.Abs((player.x) - blocks[i].collider.rightViewCoords[0, 0]) <= game.epsilon && (player.y <= maxY && player.y >= minY || player.y + player.sizeY <= maxY && player.y + player.sizeY >= minY))
                    {
                        player.colliderXleft = true;
                        right = i;
                        label3.Text = "Kolizja œciany prawej";
                    }
                    else
                    {
                        player.colliderXleft = false;
                        right = -1;
                    }
                }
                    



                // If hit top wall
                if (top == -1 || top == i)
                {
                    maxX = blocks[i].collider.topViewCoords[0, 0];
                    minX = blocks[i].collider.topViewCoords[0, 0];

                    for (int j = 0; j <= blocks[i].collider.topViewCoords.GetLength(1); j++)
                    {
                        if (maxX < blocks[i].collider.topViewCoords[j, 0])
                            maxX = blocks[i].collider.topViewCoords[j, 0];
                        if (minX > blocks[i].collider.topViewCoords[j, 0])
                            minX = blocks[i].collider.topViewCoords[j, 0];
                    }


                    if (player.x + player.sizeX >= minX && player.x <= maxX && Math.Abs(player.y - blocks[i].collider.topViewCoords[0, 1]) <= game.epsilon)
                    {
                        player.colliderYbottom = true;
                        top = i;
                        label3.Text = "Kolizja œciany górnej";
                    }
                    else
                    {
                        player.colliderYbottom = false;
                        top = -1;
                    }
                }


                // If hit bottom wall
                if (bottom == -1 || bottom == i)
                {
                    maxX = blocks[i].collider.bottomViewCoords[0, 0];
                    minX = blocks[i].collider.bottomViewCoords[0, 0];

                    for (int j = 0; j <= blocks[i].collider.topViewCoords.GetLength(1); j++)
                    {
                        if (maxX < blocks[i].collider.bottomViewCoords[j, 0])
                            maxX = blocks[i].collider.bottomViewCoords[j, 0];
                        if (minX > blocks[i].collider.bottomViewCoords[j, 0])
                            minX = blocks[i].collider.bottomViewCoords[j, 0];
                    }


                    if (player.x + player.sizeX >= minX && player.x <= maxX && Math.Abs((player.y + player.sizeY) - blocks[i].collider.bottomViewCoords[0, 1]) <= game.epsilon)
                    {
                        player.colliderYtop = true;
                        bottom = i;
                        label3.Text = "Kolizja œciany dolnej";
                    }
                    else
                    {
                        player.colliderYtop = false;
                        bottom = -1;
                    }
                }
            }
        }


        

        private void DrawPlayer(OpenGL gl)
        {

            if (player.back)
            {
                gl.Translate(player.x + player.sizeX, player.y, player.z);
                gl.Rotate(0, 180, 0);
            }
            else
                gl.Translate(player.x, player.y, player.z);
            player.DisplayModel(gl, player.modelStandard);
            player.DrawColliders(gl);
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
                gl.GenTextures(255, game.tex);
                game.LoadTexture("assets/textures/wall.jpg", 0);

                init = false;
            }

            // Do all operation before draw
            CheckCollider(gl, player, blocks);
            MovePlayer(gl);
            
            

            

            gl.LookAt(camera.eyeX, camera.eyeY, camera.eyeZ, camera.centerX, camera.centerY, camera.centerZ, camera.upX, camera.upY, camera.upZ);
            game.DrawHelpfulLines(gl);
            gl.PushMatrix();
            DrawPlayer(gl);
            gl.PopMatrix();
            game.Draw(gl, blocks);
            label1.Text = player.x + " ";
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        #region Keyboard evenets
        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
                if (!player.colliderXleft)
                    left = true;

            if (e.KeyCode == Keys.D)
                if(!player.colliderXright)
                    right = true;

            if (e.KeyCode == Keys.Space)
            {
                if (!player.isJumping && player.colliderYbottom )
                {
                    player.isJumping = true;
                    player.jumpingLimit = player.y + player.jumpMax;
                }
            }
                
                    
        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                left = false;

            if (e.KeyCode == Keys.D)
                right = false;

        }
        #endregion
    }
}