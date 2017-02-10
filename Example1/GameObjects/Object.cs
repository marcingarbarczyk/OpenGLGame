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
        public Model modelFire { get; set; }
        public Model[] modelWalk { get; set; }
        public TimeSpan walkTime { get; set; }
        public bool isWalking = false;

        public uint modelTexture { get; set; }
    }
}
