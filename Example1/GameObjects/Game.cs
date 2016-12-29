using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using System.Drawing;
using System.Drawing.Imaging;
using Example1.GameObjects;


namespace Example1.GameObjects
{
    class Game
    {
        public OpenGLCtrl ctrl { get; set; } // opengl context
        public float epsilon { get; set; } // for comparing the distance of collision
        public uint[] tex = new uint[255]; // array with textures
        public int life; // Life player
        public float[] checkpoint; // Latest checkpoint
        public float renderDistance;
        public int render;
        public bool devMode = false;
        public bool left, right, space = false;
        public Camera camera { get; set; }


        public void DrawHelpfulLines(OpenGL gl)
        {
            gl.Color(1.0, 1.0, 0);
            gl.Begin(OpenGL.LINES);
            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, 500);
            gl.Vertex(0, 0, 0);
            gl.Vertex(500, 0, 0);
            gl.Vertex(0, -500, 0);
            gl.Vertex(0, 500, 0);
            gl.End();
            gl.Color(1.0, 1.0, 1.0);
        }

        public void LoadTexture(string fn, int numer)
        {
            SharpGL.OpenGL gl = ctrl.OpenGL;
            gl.BindTexture(OpenGL.TEXTURE_2D, tex[numer]);
            Bitmap obrazek1 = new Bitmap(fn);
            Bitmap obrazek2 = new Bitmap(512, 512, PixelFormat.Format24bppRgb);

            Color c;
            for (int y = 0; y < obrazek2.Height; y++)
                for (int x = 0; x < obrazek2.Width; x++)
                {

                    c = obrazek1.GetPixel((int)(((double)x / (double)obrazek2.Width) * obrazek1.Width), (int)(((double)y / (double)obrazek2.Height) * obrazek1.Height));
                    obrazek2.SetPixel(x, y, Color.FromArgb(c.B, c.G, c.R));

                }

            gl.TexImage2D(OpenGL.TEXTURE_2D, 0, 3, obrazek2.Width, obrazek2.Height, 0, OpenGL.RGB, OpenGL.UNSIGNED_BYTE,
            obrazek2.LockBits(new Rectangle(0, 0, obrazek2.Width, obrazek2.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb).Scan0);

            gl.TexParameter(OpenGL.TEXTURE_2D, OpenGL.TEXTURE_MIN_FILTER, OpenGL.LINEAR);
            gl.TexParameter(OpenGL.TEXTURE_2D, OpenGL.TEXTURE_MAG_FILTER, OpenGL.LINEAR);
        }

        public Model LoadModel(string src)
        {
            Model model = new GameObjects.Model();
            string plik1 = "";

            try
            {
                plik1 = System.IO.File.ReadAllText(src);
            }
            catch (Exception ex)
            {
                return null;
            }

            string plik = "";
            foreach (char c in plik1)
                if (c != '.')
                    plik += c;
                else
                    plik += ',';

            string[] linie = plik.Split('\n');

            double x = 0, y = 0, z = 0;
            int ile_v = 0;

            foreach (string linia in linie)
            {
                string[] słowa = linia.Split(' ');

                if (słowa.Length < 2)
                    continue;

                if (słowa[0] == "v")
                {
                    ModelCoords.XYZ xyz = new ModelCoords.XYZ();
                    xyz.X = double.Parse(słowa[1], System.Globalization.NumberStyles.Any);
                    xyz.Y = double.Parse(słowa[2]);
                    xyz.Z = double.Parse(słowa[3]);
                    model.lista_xyz.Add(xyz);

                    x += xyz.X; y += xyz.Y; z += xyz.Z; ile_v++;
                }

                if (słowa[0] == "vt")
                {
                    ModelCoords.UV uv = new ModelCoords.UV();
                    uv.U = double.Parse(słowa[1]);
                    uv.V = double.Parse(słowa[2]);

                    model.lista_uv.Add(uv);
                }

                if (słowa[0] == "vn")
                {
                    ModelCoords.XYZ xyz = new ModelCoords.XYZ();
                    xyz.X = double.Parse(słowa[1]);
                    xyz.Y = double.Parse(słowa[2]);
                    xyz.Z = double.Parse(słowa[3]);
                    model.lista_norm.Add(xyz);
                }

                if (słowa[0] == "f")
                {
                    List<ModelCoords.Vertex> face = new List<ModelCoords.Vertex>();
                    for (int i = 1; i < słowa.Length; i++)
                    {

                        string[] liczba = słowa[i].Split('/');
                        ModelCoords.Vertex v = new ModelCoords.Vertex();
                        v.V = int.Parse(liczba[0]);
                        v.VT = int.Parse(liczba[1]);
                        v.VN = int.Parse(liczba[2]);
                        face.Add(v);
                    }
                    model.lista_f.Add(face);
                }

            }

            return model;
        }

        public void CheckDistance(Player player)
        {
            if(player.x > this.renderDistance)
            {
                this.render++;
                this.renderDistance += this.renderDistance;
            }
        }

        public void DrawBackground(OpenGL gl)
        {
            gl.Color(1.0, -15, 0);
            gl.Begin(OpenGL.QUADS);
            gl.Vertex(-30, -15, -5);
            gl.Vertex(30, -15, -5);
            gl.Vertex(30, 15, -5);
            gl.Vertex(-30, 15, -5);
            gl.End();
            gl.Color(1.0, 1.0, 1.0);
        }

        public void DrawBlocks(OpenGL gl, Level level, int index)
        {
            gl.Color(1.0, 1.0, 1.0);

            for (int j = 0; j <= index; j++)
            {
                for (int i = 0; i < level.parts[j].blocks.Count; i++)
                {
                    if (this.devMode)
                    {
                        gl.Color(0, 1.0, 0);
                        gl.Begin(OpenGL.QUADS);
                        // Top block
                        gl.Vertex(level.parts[j].blocks[i].collider.topViewCoords[0, 0], level.parts[j].blocks[i].collider.topViewCoords[0, 1], level.parts[j].blocks[i].collider.topViewCoords[0, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.topViewCoords[1, 0], level.parts[j].blocks[i].collider.topViewCoords[1, 1], level.parts[j].blocks[i].collider.topViewCoords[1, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.topViewCoords[2, 0], level.parts[j].blocks[i].collider.topViewCoords[2, 1], level.parts[j].blocks[i].collider.topViewCoords[2, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.topViewCoords[3, 0], level.parts[j].blocks[i].collider.topViewCoords[3, 1], level.parts[j].blocks[i].collider.topViewCoords[3, 2]);

                        // Bottom block
                        gl.Vertex(level.parts[j].blocks[i].collider.bottomViewCoords[0, 0], level.parts[j].blocks[i].collider.bottomViewCoords[0, 1], level.parts[j].blocks[i].collider.bottomViewCoords[0, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.bottomViewCoords[1, 0], level.parts[j].blocks[i].collider.bottomViewCoords[1, 1], level.parts[j].blocks[i].collider.bottomViewCoords[1, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.bottomViewCoords[2, 0], level.parts[j].blocks[i].collider.bottomViewCoords[2, 1], level.parts[j].blocks[i].collider.bottomViewCoords[2, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.bottomViewCoords[3, 0], level.parts[j].blocks[i].collider.bottomViewCoords[3, 1], level.parts[j].blocks[i].collider.bottomViewCoords[3, 2]);

                        // Left block
                        gl.Vertex(level.parts[j].blocks[i].collider.leftViewCoords[0, 0], level.parts[j].blocks[i].collider.leftViewCoords[0, 1], level.parts[j].blocks[i].collider.leftViewCoords[0, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.leftViewCoords[1, 0], level.parts[j].blocks[i].collider.leftViewCoords[1, 1], level.parts[j].blocks[i].collider.leftViewCoords[1, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.leftViewCoords[2, 0], level.parts[j].blocks[i].collider.leftViewCoords[2, 1], level.parts[j].blocks[i].collider.leftViewCoords[2, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.leftViewCoords[3, 0], level.parts[j].blocks[i].collider.leftViewCoords[3, 1], level.parts[j].blocks[i].collider.leftViewCoords[3, 2]);

                        // Right block
                        gl.Vertex(level.parts[j].blocks[i].collider.rightViewCoords[0, 0], level.parts[j].blocks[i].collider.rightViewCoords[0, 1], level.parts[j].blocks[i].collider.rightViewCoords[0, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.rightViewCoords[1, 0], level.parts[j].blocks[i].collider.rightViewCoords[1, 1], level.parts[j].blocks[i].collider.rightViewCoords[1, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.rightViewCoords[2, 0], level.parts[j].blocks[i].collider.rightViewCoords[2, 1], level.parts[j].blocks[i].collider.rightViewCoords[2, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.rightViewCoords[3, 0], level.parts[j].blocks[i].collider.rightViewCoords[3, 1], level.parts[j].blocks[i].collider.rightViewCoords[3, 2]);

                        // Front block
                        gl.Vertex(level.parts[j].blocks[i].collider.frontViewCoords[0, 0], level.parts[j].blocks[i].collider.frontViewCoords[0, 1], level.parts[j].blocks[i].collider.frontViewCoords[0, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.frontViewCoords[1, 0], level.parts[j].blocks[i].collider.frontViewCoords[1, 1], level.parts[j].blocks[i].collider.frontViewCoords[1, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.frontViewCoords[2, 0], level.parts[j].blocks[i].collider.frontViewCoords[2, 1], level.parts[j].blocks[i].collider.bottomViewCoords[2, 2]);
                        gl.Vertex(level.parts[j].blocks[i].collider.frontViewCoords[3, 0], level.parts[j].blocks[i].collider.frontViewCoords[3, 1], level.parts[j].blocks[i].collider.frontViewCoords[3, 2]);

                        gl.End();
                        gl.End();
                        gl.Color(1, 1, 1);
                    }
                    else
                    {
                        gl.PushMatrix();
                        gl.Translate(level.parts[j].blocks[i].collider.leftViewCoords[0, 0], level.parts[j].blocks[i].collider.topViewCoords[0, 1], 0);
                        level.parts[j].blocks[i].model.DisplayModel(gl);
                        gl.PopMatrix();
                    }
                }
            }
        }

        public void DrawPlayer(OpenGL gl, Player player)
        {
            if (player.back)
            {
                gl.Translate(player.x + player.sizeX, player.y, player.z);
                gl.Rotate(0, 180, 0);
            }
            else
                gl.Translate(player.x, player.y, player.z);

            if (this.devMode)
                player.DrawColliders(gl);
            else
                player.modelStandard.DisplayModel(gl);
        }

        public void MovePlayer(OpenGL gl, Player player)
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

            if (player.y < -5)
            {
                this.Respawn(player);
            }
        }

        public void Respawn(Player player)
        {
            this.life--;
            if(life > 0)
            {
                player.x = this.checkpoint[0];
                player.y = this.checkpoint[1];
                camera.eyeX = this.checkpoint[0];
                camera.centerX = this.checkpoint[0];
            } 
        }

        public void CheckCollider(OpenGL gl, GameObjects.Player player, Level level, int index)
        {
            double maxX, maxY, maxZ, minX, minY, minZ;
            int[] left, right, top, bottom;
            left = new int[] { -1, -1 };
            right = new int[] { -1, -1 };
            top = new int[] { -1, -1 };
            bottom = new int[] { -1, -1 };

            for (int k = 0; k <= index; k++)
            {
                for (int i = 0; i < level.parts[k].blocks.Count; i++)
                {


                    // If hit left wall
                    if (left[0] == -1 || (left[0] == i && left[1] == k))
                    {
                        maxY = level.parts[k].blocks[i].collider.leftViewCoords[0, 1];
                        minY = level.parts[k].blocks[i].collider.leftViewCoords[0, 1];

                        for (int j = 0; j <= level.parts[k].blocks[i].collider.leftViewCoords.GetLength(1); j++)
                        {
                            if (maxY < level.parts[k].blocks[i].collider.leftViewCoords[j, 1])
                                maxY = level.parts[k].blocks[i].collider.leftViewCoords[j, 1];

                            if (minY > level.parts[k].blocks[i].collider.leftViewCoords[j, 1])
                                minY = level.parts[k].blocks[i].collider.leftViewCoords[j, 1];
                        }

                        if (Math.Abs((player.x + player.sizeX) - level.parts[k].blocks[i].collider.leftViewCoords[0, 0]) <= this.epsilon && ((player.y <= maxY && player.y >= minY) || (player.y + player.sizeY <= maxY && player.y + player.sizeY >= minY)))
                        {
                            player.colliderXright = true;
                            left[0] = i;
                            left[1] = k;
                        }
                        else
                        {
                            player.colliderXright = false;
                            left[0] = left[1] = -1;
                        }


                    }




                    // If hit right wall
                    if (right[0] == -1 || (right[0] == i && right[1] == k))
                    {
                        maxY = level.parts[k].blocks[i].collider.rightViewCoords[0, 1];
                        minY = level.parts[k].blocks[i].collider.rightViewCoords[0, 1];

                        for (int j = 0; j <= level.parts[k].blocks[i].collider.rightViewCoords.GetLength(1); j++)
                        {
                            if (maxY < level.parts[k].blocks[i].collider.rightViewCoords[j, 1])
                                maxY = level.parts[k].blocks[i].collider.rightViewCoords[j, 1];

                            if (minY > level.parts[k].blocks[i].collider.rightViewCoords[j, 1])
                                minY = level.parts[k].blocks[i].collider.rightViewCoords[j, 1];
                        }

                        if (Math.Abs((player.x) - level.parts[k].blocks[i].collider.rightViewCoords[0, 0]) <= this.epsilon && (player.y <= maxY && player.y >= minY || player.y + player.sizeY <= maxY && player.y + player.sizeY >= minY))
                        {
                            player.colliderXleft = true;
                            right[0] = i;
                            right[1] = k;
                        }
                        else
                        {
                            player.colliderXleft = false;
                            right[0] = right[1] = -1;
                        }
                    }




                    // If hit top wall
                    if (top[0] == -1 || (top[0] == i && top[1] == k))
                    {
                        maxX = level.parts[k].blocks[i].collider.topViewCoords[0, 0];
                        minX = level.parts[k].blocks[i].collider.topViewCoords[0, 0];

                        for (int j = 0; j <= level.parts[k].blocks[i].collider.topViewCoords.GetLength(1); j++)
                        {
                            if (maxX < level.parts[k].blocks[i].collider.topViewCoords[j, 0])
                                maxX = level.parts[k].blocks[i].collider.topViewCoords[j, 0];
                            if (minX > level.parts[k].blocks[i].collider.topViewCoords[j, 0])
                                minX = level.parts[k].blocks[i].collider.topViewCoords[j, 0];
                        }


                        if (player.x + player.sizeX >= minX && player.x <= maxX && Math.Abs(player.y - level.parts[k].blocks[i].collider.topViewCoords[0, 1]) <= this.epsilon)
                        {
                            player.colliderYbottom = true;
                            top[0] = i;
                            top[1] = k;
                        }
                        else
                        {
                            player.colliderYbottom = false;
                            top[0] = top[1] = -1;
                        }
                    }


                    // If hit bottom wall
                    if (bottom[0] == -1 || (bottom[0] == i && bottom[1] == k))
                    {
                        maxX = level.parts[k].blocks[i].collider.bottomViewCoords[0, 0];
                        minX = level.parts[k].blocks[i].collider.bottomViewCoords[0, 0];

                        for (int j = 0; j <= level.parts[k].blocks[i].collider.topViewCoords.GetLength(1); j++)
                        {
                            if (maxX < level.parts[k].blocks[i].collider.bottomViewCoords[j, 0])
                                maxX = level.parts[k].blocks[i].collider.bottomViewCoords[j, 0];
                            if (minX > level.parts[k].blocks[i].collider.bottomViewCoords[j, 0])
                                minX = level.parts[k].blocks[i].collider.bottomViewCoords[j, 0];
                        }


                        if (player.x + player.sizeX >= minX && player.x <= maxX && Math.Abs((player.y + player.sizeY) - level.parts[k].blocks[i].collider.bottomViewCoords[0, 1]) <= this.epsilon)
                        {
                            player.colliderYtop = true;
                            bottom[0] = i;
                            bottom[1] = k;
                        }
                        else
                        {
                            player.colliderYtop = false;
                            bottom[0] = bottom[1] = -1;
                        }
                    }
                }
            }
        }

    }
}