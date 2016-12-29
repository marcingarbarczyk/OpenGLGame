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
        List<GameObjects.Block> blocks = new List<GameObjects.Block>();

        GameObjects.Model[] models = new GameObjects.Model[200];
        GameObjects.Level[] levels = new GameObjects.Level[20];
        GameObjects.Game game = new GameObjects.Game();
        GameObjects.Player player = new GameObjects.Player();



        private void BasicGameSettings()
        {
            // Main game settings
            game.ctrl = this.openGLControl1;
            game.epsilon = 0.01f;
            game.checkpoint = new float[] { 0, 0 };
            game.renderDistance = 8f;
            game.render = 0;
            game.life = 3;


            // Load models
            models[0] = game.LoadModel("assets/models/key.obj");

            // Camera settings
            game.camera = new GameObjects.Camera();
            game.camera.eyeX = 0;
            game.camera.eyeY = 2;
            game.camera.eyeZ = 15;
            game.camera.centerX = 0;
            game.camera.centerY = 0;
            game.camera.centerZ = 0;
            game.camera.upX = 0;
            game.camera.upY = 1;
            game.camera.upZ = 0;

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


            // Level 0 settings
            levels[0] = new GameObjects.Level();
            levels[0].parts[0].blocks.Add(new GameObjects.Block
            {
                textureID = 1,
                model = models[0],
                collider = new GameObjects.Collider
                {
                    leftViewCoords = new float[,]
                    {
                        {0, -1, 0},
                        {0, -1, 2},
                        {0, 0, 2},
                        {0, 0, 0}
                    },
                    rightViewCoords = new float[,]
                    {
                        {4, -1, 0},
                        {4, -1, 2},
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
                        {0, -1, 0},
                        {0, -1, 2},
                        {4, -1, 2},
                        {4, -1, 0}
                    },
                    frontViewCoords = new float[,]
                    {
                        {0, -1, 2},
                        {0, 0, 2},
                        {4, 0, 2},
                        {4, -1, 2}
                    }
                }
            });

            levels[0].parts[0].blocks.Add(new GameObjects.Block
            {
                textureID = 1,
                model = models[0],
                collider = new GameObjects.Collider
                {
                    leftViewCoords = new float[,]
                    {
                        {6, -1, 0},
                        {6, -1, 2},
                        {6, 0, 2},
                        {6, 0, 0}
                    },
                    rightViewCoords = new float[,]
                    {
                        {10, -1, 0},
                        {10, -1, 2},
                        {10, 0, 2},
                        {10, 0, 0}
                    },
                    topViewCoords = new float[,]
                    {
                        {6, 0, 0},
                        {6, 0, 2},
                        {10, 0, 2},
                        {10, 0, 0}
                    },
                    bottomViewCoords = new float[,]
                    {
                        {6, -1, 0},
                        {6, -1, 2},
                        {10, -1, 2},
                        {10, -1, 0}
                    },
                    frontViewCoords = new float[,]
                    {
                        {6, -1, 2},
                        {6, 0, 2},
                        {10, 0, 2},
                        {10, -1, 2}
                    }
                }

            });

            levels[0].parts[1].blocks.Add(new GameObjects.Block
            {
                textureID = 1,
                model = models[0],
                collider = new GameObjects.Collider
                {
                    leftViewCoords = new float[,]
                    {
                        {12, -1, 0},
                        {12, -1, 2},
                        {12, 0, 2},
                        {12, 0, 0}
                    },
                    rightViewCoords = new float[,]
                    {
                        {16, -1, 0},
                        {16, -1, 2},
                        {16, 0, 2},
                        {16, 0, 0}
                    },
                    topViewCoords = new float[,]
                    {
                        {12, 0, 0},
                        {12, 0, 2},
                        {16, 0, 2},
                        {16, 0, 0}
                    },
                    bottomViewCoords = new float[,]
                    {
                        {12, -1, 0},
                        {12, -1, 2},
                        {16, -1, 2},
                        {16, -1, 0}
                    },
                    frontViewCoords = new float[,]
                    {
                        {12, -1, 2},
                        {12, 0, 2},
                        {16, 0, 2},
                        {16, -1, 2}
                    }
                }

            });
        }

        #endregion

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

            // Do all operation before draw (check collider, moving objects, create enemies etc)
            if (game.life == 0)
            {
                this.Hide();
            }

            game.CheckDistance(player);
            game.CheckCollider(gl, player, levels[0], game.render);
            game.MovePlayer(gl, player);
            
            

            

            gl.LookAt(game.camera.eyeX, game.camera.eyeY, game.camera.eyeZ, game.camera.centerX, game.camera.centerY, game.camera.centerZ, game.camera.upX, game.camera.upY, game.camera.upZ);
            if (game.devMode)
                game.DrawHelpfulLines(gl);
            game.DrawBackground(gl);
            gl.PushMatrix();
            game.DrawPlayer(gl, player);
            gl.PopMatrix();
            game.DrawBlocks(gl, levels[0], game.render);

            if(game.devMode)
            {
                playerX.Text = player.x.ToString();
                playerY.Text = player.y.ToString();
            }
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
                    game.left = true;

            if (e.KeyCode == Keys.D)
                if(!player.colliderXright)
                    game.right = true;

            if (e.KeyCode == Keys.Space)
            {
                if (!player.isJumping && player.colliderYbottom )
                {
                    player.isJumping = true;
                    player.jumpingLimit = player.y + player.jumpMax;
                }
            }

            if (e.KeyCode == Keys.L)
            {
                if (game.devMode == false)
                {
                    game.devMode = true;
                    playerX.Visible = true;
                    playerY.Visible = true;
                }
                    
                else
                {
                    game.devMode = false;
                    playerX.Visible = false;
                    playerY.Visible = false;
                }
                    
                    
            }

            if (e.KeyCode == Keys.OemMinus)
            {
                if (game.devMode)
                    game.camera.eyeZ = game.camera.eyeZ + 5;
            }

            if (e.KeyCode == Keys.Oemplus)
            {
                if (game.devMode)
                    game.camera.eyeZ = game.camera.eyeZ - 5;
            }


        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                game.left = false;

            if (e.KeyCode == Keys.D)
                game.right = false;

        }
        #endregion
    }
}