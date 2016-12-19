using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects.ModelCoords
{
    class UV
    {
        public double U, V;

        public void glTexCoord(SharpGL.OpenGL gl)
        {
            gl.TexCoord(U, V);
        }
    }
}
