using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace Example1.GameObjects
{
    class Model
    {
        public List<ModelCoords.XYZ> lista_xyz = new List<ModelCoords.XYZ>();
        public List<ModelCoords.UV> lista_uv = new List<ModelCoords.UV>();
        public List<ModelCoords.XYZ> lista_norm = new List<ModelCoords.XYZ>();
        public List<List<ModelCoords.Vertex>> lista_f = new List<List<ModelCoords.Vertex>>();

        public void DisplayModel(SharpGL.OpenGL gl)
        {
            foreach (List<ModelCoords.Vertex> face in lista_f)
            {
                if (face.Count == 3)
                    gl.Begin(OpenGL.TRIANGLES);
                if (face.Count == 4)
                    gl.Begin(OpenGL.QUADS);

                gl.Begin(OpenGL.LINE_LOOP);

                foreach (ModelCoords.Vertex v in face)
                {
                    lista_norm[v.VN - 1].glNormal(gl);
                    lista_uv[v.VT - 1].glTexCoord(gl);
                    lista_xyz[v.V - 1].glVertex(gl);
                }

                gl.End();
                gl.End();
            }
        }
    }
}
