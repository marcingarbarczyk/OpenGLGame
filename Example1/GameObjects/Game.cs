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
        public int life = 3;
        public float[,] checkpoint;



        public void DrawHelpfulLines(OpenGL gl)
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

        List<Model.XYZ> lista_xyz = new List<Model.XYZ>();
        List<Model.UV> lista_uv = new List<Model.UV>();
        List<Model.XYZ> lista_norm = new List<Model.XYZ>();
        List<List<Model.Vertex>> lista_f = new List<List<Model.Vertex>>();

        public void LoadModel()
        {
            string plik1 = "";
            try
            {
                plik1 = System.IO.File.ReadAllText("assets/models/key.obj");
            }
            catch (Exception ex)
            {
                return;
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
                    Model.XYZ xyz = new Model.XYZ();
                    xyz.X = double.Parse(słowa[1], System.Globalization.NumberStyles.Any);
                    xyz.Y = double.Parse(słowa[2]);
                    xyz.Z = double.Parse(słowa[3]);
                    lista_xyz.Add(xyz);

                    x += xyz.X; y += xyz.Y; z += xyz.Z; ile_v++;
                }

                if (słowa[0] == "vt")
                {
                    Model.UV uv = new Model.UV();
                    uv.U = double.Parse(słowa[1]);
                    uv.V = double.Parse(słowa[2]);

                    lista_uv.Add(uv);
                }

                if (słowa[0] == "vn")
                {
                    Model.XYZ xyz = new Model.XYZ();
                    xyz.X = double.Parse(słowa[1]);
                    xyz.Y = double.Parse(słowa[2]);
                    xyz.Z = double.Parse(słowa[3]);
                    lista_norm.Add(xyz);
                }

                if (słowa[0] == "f")
                {
                    List<Model.Vertex> face = new List<Model.Vertex>();
                    for (int i = 1; i < słowa.Length; i++)
                    {

                        string[] liczba = słowa[i].Split('/');
                        Model.Vertex v = new Model.Vertex();
                        v.V = int.Parse(liczba[0]);
                        v.VT = int.Parse(liczba[1]);
                        v.VN = int.Parse(liczba[2]);
                        face.Add(v);
                    }
                    lista_f.Add(face);
                }

            }
        }

        public void DisplayModel(SharpGL.OpenGL gl)
        {
            foreach (List<Model.Vertex> face in lista_f)
            {
                if (face.Count == 3)
                    gl.Begin(OpenGL.TRIANGLES);
                if (face.Count == 4)
                    gl.Begin(OpenGL.QUADS);
                gl.Begin(OpenGL.LINE_LOOP);

                foreach (Model.Vertex v in face)
                {
                    lista_norm[v.VN - 1].glNormal(gl);
                    lista_uv[v.VT - 1].glTexCoord(gl);
                    lista_xyz[v.V - 1].glVertex(gl);
                }

                gl.End();
            }
        }

        public void Draw(OpenGL gl, List<GameObjects.Block> blocks)
        {
            gl.Color(1.0, 1.0, 1.0);
            gl.Enable(OpenGL.TEXTURE_2D);
            for (int i = 0; i < blocks.Count; i++)
            {
                
                gl.BindTexture(OpenGL.TEXTURE_2D, blocks[i].textureID);
                gl.Begin(OpenGL.QUADS);

                // Top block
                gl.TexCoord(0, 0); gl.Vertex(blocks[i].collider.topViewCoords[0, 0], blocks[i].collider.topViewCoords[0, 1], blocks[i].collider.topViewCoords[0, 2]);
                gl.TexCoord(1, 0); gl.Vertex(blocks[i].collider.topViewCoords[1, 0], blocks[i].collider.topViewCoords[1, 1], blocks[i].collider.topViewCoords[1, 2]);
                gl.TexCoord(1, 1); gl.Vertex(blocks[i].collider.topViewCoords[2, 0], blocks[i].collider.topViewCoords[2, 1], blocks[i].collider.topViewCoords[2, 2]);
                gl.TexCoord(0, 1); gl.Vertex(blocks[i].collider.topViewCoords[3, 0], blocks[i].collider.topViewCoords[3, 1], blocks[i].collider.topViewCoords[3, 2]);

                // Bottom block
                gl.TexCoord(0, 0); gl.Vertex(blocks[i].collider.bottomViewCoords[0, 0], blocks[i].collider.bottomViewCoords[0, 1], blocks[i].collider.bottomViewCoords[0, 2]);
                gl.TexCoord(1, 0); gl.Vertex(blocks[i].collider.bottomViewCoords[1, 0], blocks[i].collider.bottomViewCoords[1, 1], blocks[i].collider.bottomViewCoords[1, 2]);
                gl.TexCoord(1, 1); gl.Vertex(blocks[i].collider.bottomViewCoords[2, 0], blocks[i].collider.bottomViewCoords[2, 1], blocks[i].collider.bottomViewCoords[2, 2]);
                gl.TexCoord(0, 1); gl.Vertex(blocks[i].collider.bottomViewCoords[3, 0], blocks[i].collider.bottomViewCoords[3, 1], blocks[i].collider.bottomViewCoords[3, 2]);

                // Left block
                gl.TexCoord(0, 0); gl.Vertex(blocks[i].collider.leftViewCoords[0, 0], blocks[i].collider.leftViewCoords[0, 1], blocks[i].collider.leftViewCoords[0, 2]);
                gl.TexCoord(1, 0); gl.Vertex(blocks[i].collider.leftViewCoords[1, 0], blocks[i].collider.leftViewCoords[1, 1], blocks[i].collider.leftViewCoords[1, 2]);
                gl.TexCoord(1, 1); gl.Vertex(blocks[i].collider.leftViewCoords[2, 0], blocks[i].collider.leftViewCoords[2, 1], blocks[i].collider.leftViewCoords[2, 2]);
                gl.TexCoord(0, 1); gl.Vertex(blocks[i].collider.leftViewCoords[3, 0], blocks[i].collider.leftViewCoords[3, 1], blocks[i].collider.leftViewCoords[3, 2]);

                // Right block
                gl.TexCoord(0, 0); gl.Vertex(blocks[i].collider.rightViewCoords[0, 0], blocks[i].collider.rightViewCoords[0, 1], blocks[i].collider.rightViewCoords[0, 2]);
                gl.TexCoord(1, 0); gl.Vertex(blocks[i].collider.rightViewCoords[1, 0], blocks[i].collider.rightViewCoords[1, 1], blocks[i].collider.rightViewCoords[1, 2]);
                gl.TexCoord(1, 1); gl.Vertex(blocks[i].collider.rightViewCoords[2, 0], blocks[i].collider.rightViewCoords[2, 1], blocks[i].collider.rightViewCoords[2, 2]);
                gl.TexCoord(0, 1); gl.Vertex(blocks[i].collider.rightViewCoords[3, 0], blocks[i].collider.rightViewCoords[3, 1], blocks[i].collider.rightViewCoords[3, 2]);

                // Front block
                gl.TexCoord(0, 0); gl.Vertex(blocks[i].collider.frontViewCoords[0, 0], blocks[i].collider.frontViewCoords[0, 1], blocks[i].collider.frontViewCoords[0, 2]);
                gl.TexCoord(1, 0); gl.Vertex(blocks[i].collider.frontViewCoords[1, 0], blocks[i].collider.frontViewCoords[1, 1], blocks[i].collider.frontViewCoords[1, 2]);
                gl.TexCoord(1, 1); gl.Vertex(blocks[i].collider.frontViewCoords[2, 0], blocks[i].collider.frontViewCoords[2, 1], blocks[i].collider.bottomViewCoords[2, 2]);
                gl.TexCoord(0, 1); gl.Vertex(blocks[i].collider.frontViewCoords[3, 0], blocks[i].collider.frontViewCoords[3, 1], blocks[i].collider.frontViewCoords[3, 2]);

                gl.End();
            }
            gl.Disable(OpenGL.TEXTURE_2D);
        }
    }
}
