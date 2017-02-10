using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SharpGL;
using System.Media;

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

        GameObjects.Model[] models = new GameObjects.Model[200];
        GameObjects.Level[] levels = new GameObjects.Level[20];
        GameObjects.Game game = new GameObjects.Game();
        GameObjects.Player player = new GameObjects.Player();


        private void BasicInit(GameObjects.Game game, OpenGL gl)
        {
            game.level = 0;
            game.labelPoints = lblPoints;
            game.labelLevel = lblLevel;
            game.LoadTexturesAndModels(gl, models);
            game.epsilon = 0.01f;
            game.points = 0;
            game.life = 3;
            game.ShowPoints();

            game.LoadLevels(levels, models);
        }
        

        private void BasicLevelSettings(OpenGL gl)
        {
            // Main game settings

            if (levels[game.level] != null)
            {
                game.checkpoint = new float[] { 0, 0 };
                game.render = 0;
                game.BasicGameSettings(player, models);
                game.PlayLevelMusic(gl, levels[game.level]);
                game.ChangeLevelText();
            }
        }

        #endregion
        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Enable(OpenGL.DEPTH_TEST);

            // Init game basic settings
            if(init)
            {
                BasicInit(game, gl);
                BasicLevelSettings(gl);
                init = false;
            }

            // Check end game
            if (game.life == 0)
            {
                this.Hide();
                End end = new Example1.End();
                end.points = game.points;
                end.endText = "Przegrana! Utracono wszystkie ¿ycia!";
                end.victory = false;
                end.Show();
            }
            if (game.CheckEndLevel(player, levels[game.level]))
            {
                game.level++;
                if (game.level <= game.levelMax - 1)
                    BasicLevelSettings(gl);
                else
                {
                    game.level = 0;
                    this.Hide();
                    End end = new Example1.End();
                    end.points = game.points;
                    end.victory = true;
                    end.endText = "Gratulacje! Ukoñczy³eœ grê!";
                    end.Show();
                }
            }
            else
            {

                // Check colliders, checkpoints etc.
                game.HideLevelMessage();
                game.CheckDistance(player);
                game.CheckCollider(gl, player, levels[game.level], game.render);
                game.CheckEnemiesCollider(gl, levels[game.level], game.render);
                game.CheckEnemyAttack(gl, levels[game.level], player, game.render);
                game.CheckMedkitGetting(gl, player, levels[game.level], game.render);
                game.CheckPointGetting(gl, player, levels[game.level], game.render);
                if (player.bullets.Count > 0)
                {
                    game.CheckBulletsCollider(gl, player, levels[game.level], game.render);
                    game.CheckEnemyBulletHit(gl, player, levels[game.level], game.render);
                }
                game.CheckCheckpoints(gl, player, levels[game.level]);

                // Move objects
                game.MovePlayer(gl, player);
                game.MoveEnemy(gl, levels[game.level], game.render);
                if(player.bullets.Count > 0)
                    game.MoveBullets(gl, player);

                // Draw scene
                gl.LookAt(game.camera.eyeX, game.camera.eyeY, game.camera.eyeZ, game.camera.centerX, game.camera.centerY, game.camera.centerZ, game.camera.upX, game.camera.upY, game.camera.upZ);
                game.DrawHelpfulLines(gl);
                game.DrawBackground(gl, levels[game.level]);
                game.DrawPlayer(gl, player);
                game.DrawBullets(gl, player.bullets);
                game.DrawEnemies1(gl, levels[game.level], game.render);
                game.DrawMedkits(gl, levels[game.level], game.render);
                game.DrawPoints(gl, levels[game.level], game.render);
                game.DrawElements(gl, levels[game.level], game.render);


                if (game.devMode)
                {
                    playerX.Text = player.x.ToString();
                    playerY.Text = player.y.ToString();
                }

                // Draw UI
                game.Draw2DUI(gl);
            }
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #region Keyboard evenets
        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                if (!player.colliderXleft)
                {
                    if(!player.isWalking)
                        player.walkTime = DateTime.Now.TimeOfDay;

                    game.left = true;
                    player.isWalking = true;
                }

            if (e.KeyCode == Keys.D)
                if (!player.colliderXright)
                {
                    if (!player.isWalking)
                        player.walkTime = DateTime.Now.TimeOfDay;

                    game.right = true;
                    player.isWalking = true;
                }

            if (e.KeyCode == Keys.W)
            {
                if (!player.isJumping && player.colliderYbottom )
                {
                    if (player.jumpSound != "none")
                    {
                        var playerwmp = new WMPLib.WindowsMediaPlayer();
                        playerwmp.URL = player.jumpSound;
                        playerwmp.controls.play();
                    }
                    player.isJumping = true;
                    player.jumpingLimit = player.y + player.jumpMax;
                }
            }

            if (e.KeyCode == Keys.Space)
            {
                player.Shot();
                player.isFiring = true;
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
            {
                game.left = false;
                player.isWalking = false;
            }

            if (e.KeyCode == Keys.D)
            {
                game.right = false;
                player.isWalking = false;
            }

            if (e.KeyCode == Keys.Space)
                player.isFiring = false;

        }
        #endregion

        private void GameWindow_Load(object sender, EventArgs e)
        {
            lblLevel.Visible = false;
            lblPoints.Visible = false;
        }
    }
}