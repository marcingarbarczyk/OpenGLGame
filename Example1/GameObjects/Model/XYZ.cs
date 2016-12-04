using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects.Model
{
    class XYZ
    {
        public double X, Y, Z;

        public void glVertex(SharpGL.OpenGL gl)
        {
            gl.Vertex(X, Y, Z);
        }

        public void glNormal(SharpGL.OpenGL gl)
        {
            gl.Normal3d(X, Y, Z);
        }
    }
}
