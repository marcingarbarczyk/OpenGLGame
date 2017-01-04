using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1.GameObjects
{
    class Object
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public bool back { get; set; }

        public float sizeX { get; set; }
        public float sizeY { get; set; }
        public float sizeZ { get; set; }

        public bool colliderXleft { get; set; }
        public bool colliderXright { get; set; }
        public bool colliderYbottom { get; set; }
        public bool colliderYtop { get; set; }
        public bool colliderZ { get; set; }

        public Model modelStandard { get; set; }
        public Model modelJump { get; set; }

        public uint modelTexture { get; set; }

        public void DrawColliders(OpenGL gl)
        {
            gl.Begin(OpenGL.QUADS);
            gl.Color(1.0, 1.0, 0.0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(sizeX, 0, 0);
            gl.Vertex(sizeX, 0, sizeZ);
            gl.Vertex(0, 0, sizeZ);

            gl.Vertex(0, sizeY, 0);
            gl.Vertex(sizeX, sizeY, 0);
            gl.Vertex(sizeX, sizeY, sizeZ);
            gl.Vertex(0, sizeY, sizeZ);

            gl.Vertex(0, 0, 0);
            gl.Vertex(0, 0, sizeZ);
            gl.Vertex(0, 1, sizeZ);
            gl.Vertex(0, sizeY, 0);

            gl.Vertex(sizeX, 0, 0);
            gl.Vertex(sizeX, 0, sizeZ);
            gl.Vertex(sizeX, sizeY, sizeZ);
            gl.Vertex(sizeX, sizeY, 0);

            gl.Vertex(0, 0, sizeZ);
            gl.Vertex(sizeX, 0, sizeZ);
            gl.Vertex(sizeX, 1, sizeZ);
            gl.Vertex(0, 1, sizeZ);
            gl.End();
        }
    }
}
