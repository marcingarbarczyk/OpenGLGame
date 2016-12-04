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
        public OpenGLCtrl ctrl { get; set; }
        uint[] tex = new uint[255]; // zmienna przechowujaca textury

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
    }
}
